#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.ImplicitLockSample {

    // Hospital roster: the lock nobody can forget, because nobody takes it.
    //
    // The three patterns before this one all assume that the code doing the editing remembers to lock. In a
    // system with eleven places that write a roster — the editor, the swap process, the bank-staff import,
    // the three admin tools, a nightly job — that assumption is a bet, and it only has to be lost once.
    //
    // One forgotten lock is not a degraded guarantee. It is the guarantee gone: the whole point of the
    // other three is that a conflict is impossible, and a single unlocked write makes it possible again,
    // for every user, silently.
    //
    // An IMPLICIT LOCK takes the acquisition out of application code and puts it in a layer everything
    // passes through. Below, the unit of work locks what it is about to write; no calling code mentions a
    // lock, so no calling code can omit one.
    //
    // The reasoning is the one any repository makes for centralising a rule that must never be skipped —
    // and reviewing every path forever is not a control. It is the same argument, applied to the rule whose
    // failure is silent.
    //
    // What it costs is that the locking becomes invisible: a developer reading the roster editor sees no
    // lock, and has to know the framework takes one. That is precisely what the annotation is for.

    /// <summary>
    ///     The layer every write passes through, and where the lock is taken.
    /// </summary>
    /// <remarks>
    ///     No caller acquires a lock, so no caller can forget to. Read this before concluding that the
    ///     roster editor is unlocked — it is locked here.
    /// </remarks>
    [ImplicitLock]
    public interface IRosterUnitOfWork {

        void RegisterDirty(long rosterId);

        /// <summary>
        ///     Acquires whatever is needed for everything registered, then writes — in that order.
        /// </summary>
        void Commit(string editor);

    }

}
