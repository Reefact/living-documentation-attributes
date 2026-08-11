#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.Posa2;

#endregion

namespace DesignPatternCatalog.Usage.Posa2.InterceptorSample {

    // Every instruction the traffic service issues to a vessel is legally an act of the harbour master.
    // The inquiry after a grounding asks who ordered what, at which minute, on what information — and the
    // answer has to be recorded whether or not the developer of a new instruction remembered to record
    // it.
    //
    // Recording inside each instruction was tried. Twenty-two instruction types, nineteen of which logged;
    // the two that mattered at the inquiry were among the three that did not.
    //
    // INTERCEPTOR moves it off the instruction and onto the framework that dispatches instructions. A new
    // instruction is audited because it is dispatched, not because its author remembered.

    /// <summary>
    ///     The hook the dispatching framework calls out through.
    /// </summary>
    /// <remarks>
    ///     Implementations are never called by the application, which is what makes their effects hard to
    ///     find from a stack trace and worth annotating: a reader wondering why an instruction was refused
    ///     has nowhere in the instruction's own code to look.
    /// </remarks>
    [Interceptor.Interceptor]
    public interface IInstructionInterceptor {

        void OnInstruction(InstructionContext context);

    }

    /// <summary>
    ///     What an interceptor is told about the instruction, and what it may change.
    /// </summary>
    /// <remarks>
    ///     The whole of an interceptor's authority. What this type exposes is what the framework has agreed
    ///     to let an outsider decide — here, that an instruction may be refused, which is a power somebody
    ///     granted deliberately and could not take back without breaking every interceptor.
    /// </remarks>
    [Interceptor.Context(Interceptor = typeof(IInstructionInterceptor))]
    public sealed class InstructionContext {

        public InstructionContext(string vesselId, string instruction, string issuedBy) {
            VesselId    = vesselId;
            Instruction = instruction;
            IssuedBy    = issuedBy;
        }

        public string VesselId { get; }

        public string Instruction { get; }

        public string IssuedBy { get; }

        public bool Refused { get; private set; }

        public void Refuse() {
            Refused = true;
        }

    }

    /// <summary>
    ///     Writes the legal record of every instruction.
    /// </summary>
    /// <remarks>
    ///     Runs on the framework's path rather than on any instruction's, so what it costs is paid by every
    ///     instruction and attributed to none of them. A slow write here reads, in a profile, as the whole
    ///     service being slow.
    /// </remarks>
    [Interceptor.ConcreteInterceptor(Interceptor = typeof(IInstructionInterceptor))]
    public sealed class AuditTrailInterceptor : IInstructionInterceptor {

        private readonly IList<string> _record;

        public AuditTrailInterceptor(IList<string> record) {
            _record = record;
        }

        public void OnInstruction(InstructionContext context) {
            _record.Add($"{context.IssuedBy} → {context.VesselId}: {context.Instruction}");
        }

    }

    /// <summary>
    ///     Holds the interceptors and hands each event to them in turn.
    /// </summary>
    /// <remarks>
    ///     Registration order is execution order, and that ordering is a decision nothing else in the
    ///     codebase records: an interceptor that refuses an instruction stops the ones behind it from ever
    ///     seeing it, so whether the audit runs before or after the authority check is a policy expressed
    ///     only as the order of two calls at start-up.
    /// </remarks>
    [Interceptor.Dispatcher(Interceptor = typeof(IInstructionInterceptor))]
    public sealed class InterceptorDispatcher {

        private readonly List<IInstructionInterceptor> _attached = new List<IInstructionInterceptor>();

        public void Attach(IInstructionInterceptor interceptor) {
            _attached.Add(interceptor);
        }

        public void Detach(IInstructionInterceptor interceptor) {
            _attached.Remove(interceptor);
        }

        public void Deliver(InstructionContext context) {
            foreach (IInstructionInterceptor interceptor in _attached) {
                interceptor.OnInstruction(context);
                if (context.Refused) { return; }
            }
        }

    }

    /// <summary>
    ///     Dispatches instructions to vessels, and opens that path to interceptors.
    /// </summary>
    /// <remarks>
    ///     The point at which it calls out is an interface as real as any method signature: moving it, or
    ///     adding a second one, changes what every interceptor sees. That is the part of a framework which
    ///     cannot be changed quietly, and the annotation is where a reader is told so.
    /// </remarks>
    [Interceptor.Framework(Interceptor = typeof(IInstructionInterceptor))]
    public sealed class InstructionService {

        private readonly InterceptorDispatcher _dispatcher;

        public InstructionService(InterceptorDispatcher dispatcher) {
            _dispatcher = dispatcher;
        }

        public bool Issue(string vesselId, string instruction, string issuedBy) {
            InstructionContext context = new InstructionContext(vesselId, instruction, issuedBy);
            _dispatcher.Deliver(context);

            return !context.Refused;
        }

    }

}
