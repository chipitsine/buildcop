using System.Diagnostics;

using BuildCop.Configuration;
using BuildCop.Reporting;

namespace BuildCop.Formatters
{
    /// <summary>
    /// A base class for an XSLT file-based formatter.
    /// </summary>
    public abstract class XsltFilebasedFormatter : BaseFormatter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XsltFilebasedFormatter"/> class.
        /// </summary>
        /// <param name="configuration">The configuration for this formatter.</param>
        protected XsltFilebasedFormatter(formatterElement configuration)
            : base(configuration)
        {
        }

        #endregion

        #region WriteReport

        /// <summary>
        /// Writes the specified BuildCop report.
        /// </summary>
        /// <param name="report">The report to write.</param>
        /// <param name="minimumLogLevel">The minimum log level to write.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public sealed override void WriteReport(BuildCopReport report, LogLevel minimumLogLevel)
        {
            WriteReportCore(report, minimumLogLevel);

            if (Configuration.output.launch)
            {
                string fileName = Configuration.output.fileName;
                Process.Start(fileName);
            }
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Writes the specified BuildCop report.
        /// </summary>
        /// <param name="report">The report to write.</param>
        /// <param name="minimumLogLevel">The minimum log level to write.</param>
        /// <remarks>
        /// Override this method to write the report. The <see cref="FilebasedFormatter"/> base
        /// class will ensure that the file is launched after this method is called, depending on
        /// the outputElement.launch configuration setting.
        /// </remarks>
        protected abstract void WriteReportCore(BuildCopReport report, LogLevel minimumLogLevel);

        #endregion
    }
}