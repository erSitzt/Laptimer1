using Symbol.RFID3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;

namespace Laptimer1
{
    public partial class Form1 : Form
    {
        RFIDReader rfid3;
        Dictionary<String, Tag> tagsdict;
        private RadioButton[] antennas = new RadioButton[10];

        private UpdateRead UpdateReadHandler = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string hostname = "192.168.178.100";


            UpdateReadHandler = new UpdateRead(myUpdateRead);

            rfid3 = new RFIDReader(hostname, 5084, 2000);

            tagsdict = new Dictionary<String, Tag>();


            rfid3.Connect();


            listBox1.Items.Add(String.Format("FirwareVersion={0}", rfid3.ReaderCapabilities.FirwareVersion));

            TriggerInfo triggerInfo = new TriggerInfo();
            triggerInfo.EnableTagEventReport = true;
            triggerInfo.TagEventReportInfo.ReportNewTagEvent = TAG_EVENT_REPORT_TRIGGER.MODERATED;
            triggerInfo.TagEventReportInfo.ReportTagInvisibleEvent = TAG_EVENT_REPORT_TRIGGER.MODERATED;
            triggerInfo.TagEventReportInfo.ReportTagBackToVisibilityEvent = TAG_EVENT_REPORT_TRIGGER.MODERATED;
            triggerInfo.TagEventReportInfo.NewTagEventModeratedTimeoutMilliseconds = 200;
            triggerInfo.TagEventReportInfo.TagInvisibleEventModeratedTimeoutMilliseconds = 3000;
            triggerInfo.TagEventReportInfo.TagBackToVisibilityModeratedTimeoutMilliseconds = 200;

            // registering for read tag data notification

            rfid3.Events.ReadNotify += new Events.ReadNotifyHandler(Events_ReadNotify);

            // ReadNotify Event comes without tag data 

            rfid3.Events.AttachTagDataWithReadEvent = true;

            rfid3.Actions.Inventory.Perform(null, triggerInfo, null);
            timer1.Start();
        }
        private delegate void Update();
        public void addlist(string item)
        {
            listBox2.Invoke(new Update(() => listBox2.Items.Add(item)));
        }
        private delegate void UpdateRead(Events.ReadEventData eventData);
        private async void  myUpdateRead(Events.ReadEventData eventData)
        {
            if (rfid3.Events.AttachTagDataWithReadEvent)
            {
                addlist(String.Format("TagID : {0} : {1} : {2} : {3} : {4}", eventData.TagData.TagID, eventData.TagData.AntennaID, eventData.TagData.TagEvent, eventData.TagData.tagTransitionStatus, eventData.TagData.TagEventTimeStamp));
                Tag tmptag = new Tag(eventData.TagData.TagID, eventData.TagData.TagEventTimeStamp);
                //tagsdict.Add(tmptag.TagId, tmptag);
                if (eventData.TagData.OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ && eventData.TagData.OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS)
                {
                    addlist(String.Format("{0} : {1}", eventData.TagData.MemoryBank.ToString(), eventData.TagData.MemoryBankData));

                }
                if (eventData.TagData.TagEvent == TAG_EVENT.NEW_TAG_VISIBLE || eventData.TagData.TagEvent == TAG_EVENT.TAG_BACK_TO_VISIBILITY)
                {

                    var json = JsonConvert.SerializeObject(tmptag);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var url = textBox_laptimeService.Text + "/lap/" + tmptag.TagId;
                    var client = new HttpClient();

                    var response = await client.PostAsync(url, data);
                    var result = await response.Content.ReadAsStringAsync();




                }
            }
            else
            {
                Symbol.RFID3.TagData[] tagData = rfid3.Actions.GetReadTags(1000);
                objectListView1.SetObjects(tagData);
            }
            


        }
        public void Events_ReadNotify(object sender, Events.ReadEventArgs readEventArgs)
        {
            try
            {
                this.Invoke(UpdateReadHandler, new object[] { readEventArgs.ReadEventData });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            /*
            addlist(String.Format("TagID : {0} : {1} : {2} : {3} : {4}", e.ReadEventData.TagData.TagID, e.ReadEventData.TagData.AntennaID, e.ReadEventData.TagData.TagEvent, e.ReadEventData.TagData.tagTransitionStatus, e.ReadEventData.TagData.TagEventTimeStamp));
            Tag tmptag = new Tag(e.ReadEventData.TagData.TagID);
            //tagsdict.Add(tmptag.TagId, tmptag);
            if (e.ReadEventData.TagData.OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ && e.ReadEventData.TagData.OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS)
            {
                addlist(String.Format("{0} : {1}", e.ReadEventData.TagData.MemoryBank.ToString(), e.ReadEventData.TagData.MemoryBankData));

            }
            */

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            objectListView1.BuildList();
            listBox3.Items.Clear();
            TagData[] seenTags = rfid3.Actions.GetReadTags(1000);
            if (seenTags != null)
            {
                for (int nIndex = 0; nIndex < seenTags.Length; nIndex++)
                {
                    listBox3.Items.Add(String.Format("{0} : Seen : {1}", seenTags[nIndex].TagID, seenTags[nIndex].TagSeenCount));
                }
            }
        }
    }
    class Tag
    {
        public Tag(string TagId, DateTime TagSeenTime)
        {
            this.TagId = TagId;
            this.SeenCount = 0;
            this.TagSeenTime = TagSeenTime;
        }

        public string TagId { get; set; }
        public int SeenCount { get; set; }

        public DateTime TagSeenTime { get; set; }

    }
}
