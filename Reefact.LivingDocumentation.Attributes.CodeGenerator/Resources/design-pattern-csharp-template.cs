using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace {Namespace} \{

    /// <summary>
    ///     Base class for attributes used to decorate classes, interfaces or members participating
    ///     in the {Name} design pattern.
    /// </summary>
    public class {ClassName}PatternAttribute : LivingDocumentationAttribute \{ \}

    /// <summary>
    ///     Use this attribute to label the role played by the class or interface in the {Name} design pattern.
    /// </summary>
    /// <seealso cref="{ClassName}MemberAttribute" />
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public sealed class {ClassName}ParticipantAttribute : {ClassName}PatternAttribute \{ 

        /// <summary>
        ///     Initializes a new instance of the <see cref="{ClassName}ParticipantAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the {Name} design pattern.</param>
        public {ClassName}ParticipantAttribute({ClassName}Participant participant) \{
            Participant = participant;
        \}
        
        /// <summary>
        ///     The role the class or interface plays within the {Name} design pattern.
        /// </summary>
        public {ClassName}Participant Participant \{ get; \}

    \}

    /// <summary>
    ///     Use this attribute to label the role played by the member in the {Name} design pattern.
    /// </summary>
    /// <seealso cref="{ClassName}ParticipantAttribute" />
    [AttributeUsage(AttributeTargets.Method)]
    public class {ClassName}MemberAttribute : {ClassName}PatternAttribute \{ 

        /// <summary>
        ///     Initializes a new instance of the <see cref="{ClassName}MemberAttribute" /> class.
        /// </summary>
        /// <param name="member">The role the member plays within the {Name} design pattern.</param>
        public {ClassName}MemberAttribute({ClassName}Member member) \{
                Member = member;
        \}

        /// <summary>
        ///     The role the member plays within the {Name} design pattern.
        /// </summary>
        public {ClassName}Member Member \{ get; \}
    \}

    /// <summary>
    ///     Use these values as parameters for the <see cref="{ClassName}MemberAttribute" /> to indicate
    ///     the specific role a class or interface plays within the {Name} pattern.
    /// </summary>
    public enum {ClassName}Member \{
{Members:
    /// <summary>
    /// {Description}
    /// </summary>
    {EnumName}|,\r\n|,\r\n  }
    \}

    /// <summary>
    ///     Use these values as parameters for the <see cref="{ClassName}ParticipantAttribute" /> to indicate
    ///     the specific role a class or interface plays within the {Name} pattern.
    /// </summary>
    public enum {ClassName}Participant \{
{Participants:
    /// <summary>
    /// {Description}
    /// </summary>
    {EnumName}|,\r\n|,\r\n  }
    \}

\}