using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using EWSVoiceAccess.ExchangeWebService;

namespace Chapter07
{
    class ExchangeCalendar
    {
        //Force Static Method
        private ExchangeCalendar()
        {

        }
        public static List<CalendarObject> GetCalendarData(DateTime lookupDate)
        {
            List<CalendarObject> calendarObjects = new List<CalendarObject>();

            //umdemo.dnsalias.com explicit credentials
            ExchangeServiceBinding exchangeServer = new ExchangeServiceBinding();

            ICredentials creds = new NetworkCredential(user, password, "um.test.com");
            
            exchangeServer.Credentials = creds;
            exchangeServer.Url = @"http://um.test.com/ews/exchange.asmx";

            GetUserAvailabilityRequestType request = new GetUserAvailabilityRequestType();

            MailboxData[] mailboxes = new MailboxData[1];
            mailboxes[0] = new MailboxData();

            // Identify the user mailbox to review their Free/Busy data
            EmailAddress emailAddress = new EmailAddress();

            emailAddress.Address = "jason@um.test.com";

            emailAddress.Name = String.Empty;

            mailboxes[0].Email = emailAddress;

            request.MailboxDataArray = mailboxes;

            //Set TimeZone
            request.TimeZone = new SerializableTimeZone();
            request.TimeZone.Bias = 480;
            request.TimeZone.StandardTime = new SerializableTimeZoneTime();
            request.TimeZone.StandardTime.Bias = 0;
            request.TimeZone.StandardTime.DayOfWeek = DayOfWeekType.Sunday.ToString();
            request.TimeZone.StandardTime.DayOrder = 1;
            request.TimeZone.StandardTime.Month = 11;
            request.TimeZone.StandardTime.Time = "02:00:00";
            request.TimeZone.DaylightTime = new SerializableTimeZoneTime();
            request.TimeZone.DaylightTime.Bias = -60;
            request.TimeZone.DaylightTime.DayOfWeek = DayOfWeekType.Sunday.ToString();
            request.TimeZone.DaylightTime.DayOrder = 2;
            request.TimeZone.DaylightTime.Month = 3;
            request.TimeZone.DaylightTime.Time = "02:00:00";


            // Identify the time to compare if the user is Free/Busy 
            Duration duration = new Duration();
            duration.StartTime = lookupDate;
            duration.EndTime = lookupDate.AddDays(1);

            // Identify the options for comparing F/B
            FreeBusyViewOptionsType viewOptions = new FreeBusyViewOptionsType();
            viewOptions.TimeWindow = duration;

            viewOptions.RequestedView = FreeBusyViewType.Detailed;
            viewOptions.RequestedViewSpecified = true;


            request.FreeBusyViewOptions = viewOptions;

            GetUserAvailabilityResponseType response = exchangeServer.GetUserAvailability(request);

            foreach (FreeBusyResponseType responseType in response.FreeBusyResponseArray)
            {
                if (responseType.FreeBusyView.CalendarEventArray.Length > 0)
                {
                    foreach (CalendarEvent calendar in responseType.FreeBusyView.CalendarEventArray)
                    {
                        CalendarObject calendarObject = new CalendarObject();

                        calendarObject.Location = calendar.CalendarEventDetails.Location;
                        calendarObject.Subject = calendar.CalendarEventDetails.Subject;
                        calendarObject.StartDate = calendar.StartTime;
                        calendarObject.EndDate = calendar.EndTime;
                        calendarObject.IsMeeting = calendar.CalendarEventDetails.IsMeeting;

                        calendarObjects.Add(calendarObject);
                    }
                }
            }

            return calendarObjects;
        }
    }

    public class CalendarObject
    {
        private string _location;
        private string _subject;
        private bool _isMeeting;
        private DateTime _startDate;
        private DateTime _endDate;


        public CalendarObject()
        {
        }

        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }
        public bool IsMeeting
        {
            get
            {
                return _isMeeting;
            }
            set
            {
                _isMeeting = value;
            }
        }
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
            }
        }
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
            }
        }

    }
}

