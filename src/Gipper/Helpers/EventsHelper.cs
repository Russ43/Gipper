using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;


namespace Gipper
{
	static internal class EventsHelper
	{
		// document events
		static public event EventHandler<Document> DocumentClosing;

		
		// window events
		static public event EventHandler<Window> WindowCreated;
		static public event EventHandler<Tuple<Window, Window>> WindowActivated;
		static public event EventHandler<Window> WindowClosing;


		// fields
		static private DTE2 _dte;
		static private Events _events;
		static private DocumentEvents _documentEvents;
		static private WindowEvents _windowEvents;


		// methods
		static public void Initialize(DTE2 dte)
		{
			_dte = dte;

			// NOTE: DTE event classes won't work if you don't cache each object to prevent garbage collection.
			_events = dte.Events;

			// subscribe to interesting document events
			_documentEvents = _events.DocumentEvents;
			_documentEvents.DocumentClosing += DocumentEvents_DocumentClosing;

			// subscribe to interesting window events
			_windowEvents = _events.WindowEvents;
			_windowEvents.WindowCreated += WindowEvents_WindowCreated;
			_windowEvents.WindowActivated += WindowEvents_WindowActivated;
			_windowEvents.WindowClosing += WindowEvents_WindowClosing;
		}


		// document event handlers
		static private void DocumentEvents_DocumentClosing(Document document)
		{
			EventHandler<Document> documentClosingEvent = DocumentClosing;
			if(documentClosingEvent != null)
				documentClosingEvent(_dte, document);
		}


		// window event handlers
		static private void WindowEvents_WindowCreated(Window window)
		{
			EventHandler<Window> windowCreatedEvent = WindowCreated;
			if(windowCreatedEvent != null)
				windowCreatedEvent(_dte, window);
		}

		static private void WindowEvents_WindowActivated(Window gotFocus, Window lostFocus)
		{
			EventHandler<Tuple<Window, Window>> windowActivatedEvent = WindowActivated;
			if(windowActivatedEvent != null)
				windowActivatedEvent(_dte, new Tuple<Window, Window>(gotFocus, lostFocus));
		}

		static private void WindowEvents_WindowClosing(Window window)
		{
			EventHandler<Window> windowClosingEvent = WindowClosing;
			if(windowClosingEvent != null)
				windowClosingEvent(_dte, window);
		}
	}
}
