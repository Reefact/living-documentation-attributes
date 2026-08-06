#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.ModelViewControllerSample {

    // A university enrolment portal — the domain the web presentation and session state families share,
    // because those patterns are alternatives and choices, and both only read against one real site.
    //
    // MODEL VIEW CONTROLLER splits presentation into three. Everyone can recite that; what people lose is
    // that the pattern is not the three classes, it is the DIRECTION OF IGNORANCE between them.
    //
    // The model below knows nothing of the other two. That is the constraint doing the work, and the reason
    // is concrete: the enrolment is shown on the student's page, on the tutor's dashboard, and in an email.
    // Three views of one model, at once. Anything the model knew about the first would be wrong the day the
    // second appeared — so it may know none of them.
    //
    // The controller holds no rule. Whether a student may enrol is asked of the model; the controller
    // interprets the request, asks, and picks a view. A rule that lives in the controller cannot be reached
    // by anything that is not a request — not by the nightly batch, not by the admin tool, not by a test —
    // and the day the second caller appears the rule is copied.
    //
    // The links are declared because a codebase has many occurrences of this, and a reader looking at one
    // controller needs to know which model and which view it belongs to.

    /// <summary>
    ///     What the presentation is about: the enrolment itself, ignorant of everything showing it.
    /// </summary>
    [ModelViewController.Model]
    public sealed class Enrolment {

        public Enrolment(string studentNumber, string courseCode, int creditsAlreadyTaken) {
            StudentNumber       = studentNumber;
            CourseCode          = courseCode;
            CreditsAlreadyTaken = creditsAlreadyTaken;
        }

        public string StudentNumber       { get; }
        public string CourseCode          { get; }
        public int    CreditsAlreadyTaken { get; }

        /// <summary>
        ///     The rule, on the model, where every caller can reach it.
        /// </summary>
        public bool MayEnrol(int courseCredits) {
            return CreditsAlreadyTaken + courseCredits <= 180;
        }

    }

    /// <summary>
    ///     One rendering of it. There are three; the model knows none.
    /// </summary>
    [ModelViewController.View(Model = typeof(Enrolment))]
    public interface IEnrolmentView {

        string Render(Enrolment enrolment);

    }

    /// <summary>
    ///     What a request does — and nothing about what is allowed.
    /// </summary>
    [ModelViewController.Controller(Model = typeof(Enrolment), View = typeof(IEnrolmentView))]
    public sealed class EnrolmentController {

        public string Enrol(string studentNumber, string courseCode, int courseCredits) {
            Enrolment enrolment = new(studentNumber, courseCode, 0);

            // Asked, not decided.
            return enrolment.MayEnrol(courseCredits) ? "enrolment-confirmed" : "enrolment-refused";
        }

    }

}
