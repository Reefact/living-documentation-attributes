#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;

#endregion

namespace DesignPatternCatalog.Usage.DomainDrivenDesign.CohesiveMechanismSample {

    // A city's district heating network — a few hundred kilometres of insulated pipe, one plant, eleven
    // thousand buildings, and a planner who has to answer one question all day: can this new block be
    // connected without starving the end of the eastern branch in February?
    //
    // The model that answers it is small and readable — plants, pipes, substations, a demand per building.
    // The answer is not. It is a hydraulic and thermal balance over the whole network, solved iteratively,
    // and it is several hundred lines of numerics that mention nothing a planner would recognise.
    //
    // A COHESIVE MECHANISM is that solver, taken out and put behind an interface stated in the planner's
    // terms. The model asks whether a load can be served; it does not iterate.
    //
    // The thing this rescues is the model rather than the algorithm. Left in place, the numerics do not sit
    // quietly beside the concepts — they pull on them. A pipe grows a residual and a Reynolds number, a
    // substation grows a convergence flag, and after a year of that nobody can read the model to find out
    // what the business believes, because two thirds of every class is machinery. Separating them is what
    // keeps a `Pipe` a pipe.
    //
    // It also earns its keep in the other direction, though that is the lesser reason: the solver is a
    // well-documented category of algorithm, so it can be tested against published cases, replaced by a
    // faster one, or bought — and none of that touches the model.
    //
    // Two things worth noticing about the interface. It is stated in what the planner wants to know, not in
    // what the solver computes: `CanServe`, not `Solve`. And it returns a reason when the answer is no,
    // because a planner who is told "no" without being told which branch is short has been given the
    // algorithm's answer rather than the domain's.

    /// <summary>
    ///     The hydraulic and thermal balance of the network, asked in the planner's language.
    /// </summary>
    /// <remarks>
    ///     Nothing here mentions iteration, convergence or residuals. That vocabulary lives entirely behind
    ///     this interface, which is the whole of what the pattern is for.
    /// </remarks>
    [CohesiveMechanism]
    public interface INetworkCapacity {

        /// <summary>
        ///     Whether the network can carry a new load at the given connection point on the coldest design day.
        /// </summary>
        CapacityVerdict CanServe(string substation, double kilowatts);

    }

    /// <summary>
    ///     The answer, with the part a planner acts on.
    /// </summary>
    /// <param name="Served">Whether the load can be carried.</param>
    /// <param name="LimitingSection">Which stretch of pipe runs out first, when it cannot.</param>
    public sealed record CapacityVerdict(bool Served, string? LimitingSection);

    /// <summary>
    ///     A stretch of pipe, and what it stayed once the numerics moved out.
    /// </summary>
    public sealed record PipeSection(string Name, double DiameterMillimetres, double LengthMetres);

}
