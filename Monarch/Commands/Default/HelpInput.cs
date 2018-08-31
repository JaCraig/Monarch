using System.ComponentModel.DataAnnotations;

namespace Monarch.Commands.Default
{
    /// <summary>
    /// Help input
    /// </summary>
    public class HelpInput
    {
        /// <summary>
        /// Gets or sets the command to get help on.
        /// </summary>
        /// <value>The command to get help on.</value>
        [Display(Description = "The command that you wish to have information about.")]
        public string Command { get; set; }
    }
}