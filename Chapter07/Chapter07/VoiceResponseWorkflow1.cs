using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SpeechServer.Dialog;
using System.Collections.Generic;

namespace Chapter07
{
    public sealed partial class Workflow1: SpeechSequentialWorkflowActivity
    {
        private List<CalendarObject> _calendarList;

        public Workflow1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method is called when any exception other than a CallDisconnectedException occurs within the workflow.
        /// It does some generic exception logging, and is provided for convenience during debugging;
        /// you should replace or augment this with your own error handling code.
        /// CallDisconnectedExceptions are handled separately; see HandleCallDisconnected, below.
        /// </summary>
        private void HandleGeneralFault(object sender, EventArgs e)
        {
            // The fault property is read only.  When an exception is thrown the actual exception is 
            // stored in this property.  Check this value for error information.
            string errorMessage = this.generalFaultHandler.Fault is System.Reflection.TargetInvocationException && this.generalFaultHandler.Fault.InnerException != null ?
                   this.generalFaultHandler.Fault.InnerException.Message : this.generalFaultHandler.Fault.Message;

            LogError(errorMessage);
        }

        /// <summary>
        /// This method is called when a CallDisconnectedException occurs in the workflow.
        /// This happens when a speech activity tried to run while the call is not connected,
        /// and can happen for two reasons:
        /// (1) The user hung up on the app in the middle of the flow.  This is expected and normal.
        /// (2) You disconnected the call locally, and then attempted to run a speech activity.
        ///     This is an application bug, and so we break if the debugger is already attached.
        /// </summary>
        private void HandleCallDisconnected(object sender, EventArgs e)
        {
            if (((CallDisconnectedException) callDisconnectedHandler.Fault).LocalInitiation && Debugger.IsAttached)
            {
                LogError(callDisconnectedHandler.Fault.Message);
            }
        }

        /// <summary>
        /// Logs an application error and breaks into the debugger (if already attached).
        /// </summary>
        private void LogError(string errorMessage) 
        {
            if(Debugger.IsAttached)
            {
                // If the debugger is attached, break here so that you can see the error that occurred.
                // (Check the errorMessage variable.)
                Debugger.Break();
            }

            // Write the error to both the NT Event Log and the .etl file, so that some record is kept 
            // even if the debugger is not attached. The first parameter is an event id, chosen arbitrarily.
            // Microsoft Office Communication Server 2007, Speech Server uses various IDs below 50000, 
            // so you might want to use IDs above this number to avoid overlap.
            TelephonySession.LoggingManager.LogApplicationError(
                    50000,  
                    string.Format(System.Globalization.CultureInfo.CurrentCulture,
                    "An exception occurred in the Speech workflow with Id {0}. The exception was: {1}",  
                    this.WorkflowInstanceId, errorMessage));

            // Dump a detailed version of the most recent server logs to the .etl file
            // (see the 'Speech Server Administrator Console' for your current logging settings, 
            // including the location of this file)
            this.TelephonySession.LoggingManager.DumpTargetedLogs();
        }

        private void executeWebService_ExecuteCode(object sender, EventArgs e)
        {
            //Get Semantic Date Items
            int year = Convert.ToInt32(this.askDate.RecognitionResult.Semantics["Year"].Value.ToString());
            int day = Convert.ToInt32(this.askDate.RecognitionResult.Semantics["Day"].Value.ToString());
            int month = Convert.ToInt32(this.askDate.RecognitionResult.Semantics["Month"].Value.ToString());
            _calendarList = ExchangeCalendar.GetCalendarData(new DateTime(year, month, day));

        }

        private void sayAppointment_TurnStarting(object sender, TurnStartingEventArgs e)
        {
            //Read First Calendar Event, We could use the navigablelist here to read all
            //but for now we will just read the first one
            CalendarObject calendarEvent = _calendarList[0];

            this.sayAppointment.MainPrompt.ClearContent();
            this.sayAppointment.MainPrompt.AppendText("The selected appointment, {0}, starts on ", calendarEvent.Subject);
            this.sayAppointment.MainPrompt.AppendTextWithHint(calendarEvent.StartDate.ToLongDateString(), Microsoft.SpeechServer.Synthesis.SayAs.Date);
            this.sayAppointment.MainPrompt.AppendText(" at ");
            this.sayAppointment.MainPrompt.AppendBreak(Microsoft.SpeechServer.Synthesis.PromptBreak.Small);
            this.sayAppointment.MainPrompt.AppendTextWithHint(calendarEvent.StartDate.ToShortTimeString(), Microsoft.SpeechServer.Synthesis.SayAs.Time);
            this.sayAppointment.MainPrompt.AppendText(" and ending at ");
            this.sayAppointment.MainPrompt.AppendTextWithHint(calendarEvent.EndDate.ToShortTimeString(), Microsoft.SpeechServer.Synthesis.SayAs.Time);
            this.sayAppointment.MainPrompt.AppendBreak(Microsoft.SpeechServer.Synthesis.PromptBreak.Small);
            this.sayAppointment.MainPrompt.AppendText(" and it is located in ");
            this.sayAppointment.MainPrompt.AppendText(calendarEvent.Location);
            this.sayAppointment.MainPrompt.AppendBreak(Microsoft.SpeechServer.Synthesis.PromptBreak.Small);
        }
    }
}
