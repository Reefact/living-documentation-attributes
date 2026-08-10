#region Usings declarations


using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.ClientSideUiCompositionSample {

    // The same three answers, on the mobile app rather than the web site — where the network is worse and a
    // region that fails to load should not take the screen with it.
    //
    // CLIENT-SIDE UI COMPOSITION has each team ship a component and the skeleton place them. The trade
    // against the server-side alternative is exactly that: composition in the browser means a missing region
    // degrades instead of failing the page.

    /// <summary>
    ///     The balance card, rendered in the browser by the billing team's component.
    /// </summary>
    /// <remarks>
    ///     Composed on the client rather than on the server, so a component that fails to arrive degrades
    ///     one card instead of the whole response — the difference that makes this the alternative rather
    ///     than a variation.
    /// </remarks>
    [ClientSideUiComposition.UiComponent(PageSkeleton = typeof(IAccountScreenSkeleton))]
    public interface IBalanceCard {

        object Render(string supplyPoint);

    }

    /// <summary>
    ///     The reading card, from the metering team.
    /// </summary>
    [ClientSideUiComposition.UiComponent(PageSkeleton = typeof(IAccountScreenSkeleton))]
    public interface ILastReadingCard {

        object Render(string supplyPoint);

    }

    /// <summary>
    ///     The skeleton the cards are placed into.
    /// </summary>
    /// <remarks>
    ///     It decides layout and nothing else, and it has to keep working when a card does not arrive.
    ///     That obligation is the whole of what this role asserts.
    /// </remarks>
    [ClientSideUiComposition.PageSkeleton]
    public interface IAccountScreenSkeleton {

        object Compose(string supplyPoint);

    }
}
