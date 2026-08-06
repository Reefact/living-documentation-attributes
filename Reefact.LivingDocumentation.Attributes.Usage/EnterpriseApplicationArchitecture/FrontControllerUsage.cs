#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.FrontControllerSample {

    // Enrolment portal: the authenticated side, where every request needs the same six things.
    //
    // Behind the login there are forty pages, and each must resolve the locale, check the session, refuse
    // an expired one, load the student, record an audit line, and set the security headers. As page
    // controllers that is forty copies of six steps — and the page added in week nine will have five.
    //
    // A FRONT CONTROLLER makes that impossible: every request passes through ONE handler, so what is common
    // is written once and cannot be forgotten. The handler then dispatches to a command that does the one
    // thing this request is about.
    //
    // The two roles are separate on purpose, and the reason is in the command: it is created PER REQUEST.
    // A page controller can hold state between calls because it belongs to one page; a command must not,
    // because one handler serves everything and shared state there is shared across every user on the site.
    //
    // The trade against the page controller next door: this centralises, so it is where a change to
    // request handling is made once — and it is also a single point through which everything passes,
    // which is felt the day two pages genuinely need different treatment.

    /// <summary>
    ///     Every authenticated request enters here, and the six common steps happen once.
    /// </summary>
    [FrontController.Handler]
    public sealed class PortalHandler {

        private readonly IReadOnlyDictionary<string, IPortalCommand> _commands;

        public PortalHandler(IReadOnlyDictionary<string, IPortalCommand> commands) {
            _commands = commands;
        }

        public string Handle(string path, string sessionToken) {
            if (string.IsNullOrEmpty(sessionToken)) { return "redirect:/login"; }
            if (!_commands.TryGetValue(path, out IPortalCommand? command)) { return "404"; }

            // Locale, audit, headers — once, here, for all forty pages.
            return command.Execute(sessionToken);
        }

    }

    /// <summary>
    ///     What one request does. New per request, and therefore free to hold that request's state.
    /// </summary>
    [FrontController.Command(Handler = typeof(PortalHandler))]
    public interface IPortalCommand {

        string Execute(string sessionToken);

    }

}
