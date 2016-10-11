using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SpeechServer ;
using Microsoft.SpeechServer.Dialog;
using System.Workflow.Runtime;

namespace NavigableListActivity
{
    /// <summary>
    /// This class implements the IHostedSpeechApplication interface necessary for an
    /// application to be supported on 'Microsoft Office Communication Server 2007 Speech Server' platform. 
    /// This class serves as the entry point of execution from the application developer's perspective.
    /// </summary>
    public class Class1 : IHostedSpeechApplication
    {      
        private IApplicationHost _host;
        private WorkflowInstance _workflowInstance;

        /// <summary>
        /// Constructor
        /// </summary>
        public Class1()
        {
        }

        #region IHostedSpeechApplication implementation

        /// <summary>
        /// starts the application
        /// </summary>
        /// <param name="host">The ApplicationHost for this application.</param>
        public void Start(IApplicationHost host)
        {
            if (host != null)
            {
                _host = host;
                _host.TelephonySession.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                _workflowInstance = SpeechSequentialWorkflowActivity.CreateWorkflow(_host, typeof(NavigableListActivity.Workflow1));
                SpeechSequentialWorkflowActivity.Start(_workflowInstance);
            }
            else 
            {
                throw new ArgumentNullException("host");
            }
        }
 
        /// <summary>
        /// stops the application
        /// </summary>
        /// <param name="immediate">true to indicate that the application should be stopped immediately.</param>
        public void Stop(bool immediate)
        {
            SpeechSequentialWorkflowActivity.Stop(_workflowInstance, immediate);
        }

        /// <summary>
        /// Hangs up if an exception occurs outside the workflow.
        /// 
        /// For exceptions within the workflow, HandleGeneralFault in the 
        /// speech workflow file will be called. 
        /// </summary>
        /// <param name="exception">The exception that occurred</param>
        public void OnUnhandledException(Exception exception)
        {
            if (exception != null)
            {
                _host.TelephonySession.LoggingManager.LogApplicationError(100, "An unexpected exception occurred: {0}", exception.Message);
            }
            else
            {
                _host.TelephonySession.LoggingManager.LogApplicationError(100, "An unknown exception occurred: {0}", System.Environment.StackTrace);
            }

            _host.OnCompleted();
        }
        
        #endregion IHostedSpeechApplication implementation
    }
}
