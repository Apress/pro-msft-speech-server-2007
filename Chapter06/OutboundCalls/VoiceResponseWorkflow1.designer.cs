using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace OutboundCalls
{
	public sealed partial class Workflow1
	{
		#region Designer generated code
		
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode]
		private void InitializeComponent()
		{
			this.CanModifyActivities = true;
			this.faultCode = new System.Workflow.Activities.CodeActivity();
			this.callDisconnectedCode = new System.Workflow.Activities.CodeActivity();
			this.callDisconnectedHandler = new System.Workflow.ComponentModel.FaultHandlerActivity();
			this.generalFaultHandler = new System.Workflow.ComponentModel.FaultHandlerActivity();
			this.faultHandlers = new System.Workflow.ComponentModel.FaultHandlersActivity();
			this.answerCallActivity1 = new Microsoft.SpeechServer.Dialog.AnswerCallActivity();
			this.disconnectCallActivity1 = new Microsoft.SpeechServer.Dialog.DisconnectCallActivity();
			// 
			// faultCode
			// 
			this.faultCode.Name = "faultCode";
			this.faultCode.ExecuteCode += new System.EventHandler(this.HandleGeneralFault);
			//
			// callDisconnectedCode
			//
			this.callDisconnectedCode.Name = "callDisconnectedCode";
			this.callDisconnectedCode.ExecuteCode += new System.EventHandler(this.HandleCallDisconnected);
			// 
			// generalFaultHandler
			// 
			this.generalFaultHandler.Activities.Add(this.faultCode);
			this.generalFaultHandler.FaultType = typeof(System.Exception);
			this.generalFaultHandler.Name = "generalFaultHandler";
			//
			// callDisconnectedHandler
			//
			this.callDisconnectedHandler.Activities.Add(this.callDisconnectedCode);
			this.callDisconnectedHandler.FaultType = typeof(Microsoft.SpeechServer.Dialog.CallDisconnectedException);
			this.callDisconnectedHandler.Name = "callDisconnectedHandler";
			// 
			// faultHandlersActivity
			// 
			this.faultHandlers.Activities.Add(this.callDisconnectedHandler);
			this.faultHandlers.Activities.Add(this.generalFaultHandler);
			this.faultHandlers.Name = "faultHandlers";
			// 
			// answerCallActivity1
			// 
			this.answerCallActivity1.Name = "answerCallActivity1";
			// 
			// disconnectCallActivity1
			// 
			this.disconnectCallActivity1.Name = "disconnectCallActivity1";
			// 
			// Workflow1
			// 
			this.Activities.Add(this.answerCallActivity1);
			this.Activities.Add(this.disconnectCallActivity1);
			this.Activities.Add(this.faultHandlers);
			this.History = null;
			this.Name = "Workflow1";
			
			this.TaskLoggingEnabled = true;
			this.CanModifyActivities = false;
		}
		
		private Microsoft.SpeechServer.Dialog.AnswerCallActivity answerCallActivity1;
		private FaultHandlersActivity faultHandlers;
		private FaultHandlerActivity generalFaultHandler;
		private FaultHandlerActivity callDisconnectedHandler;
		private CodeActivity faultCode;
		private CodeActivity callDisconnectedCode;
		private Microsoft.SpeechServer.Dialog.DisconnectCallActivity disconnectCallActivity1;

		#endregion
	}
}
