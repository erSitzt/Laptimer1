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
        TriggerInfo triggerInfo;

        Dictionary<String, Tag> tagsdict;

        Dictionary<String, Lap> openlapsbytag;
        Dictionary<String, List<Lap>> finishedlapsbytag;

        private RadioButton[] antennas = new RadioButton[10];

        private UpdateRead UpdateReadHandler = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }
        private delegate void Update();
        public void addlist(string item)
        {
            listBox2.Invoke(new Update(() => listBox2.Items.Add(item)));
            listBox2.SelectedIndex = listBox2.Items.Count - 1;
            listBox2.SelectedIndex = -1;
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
                    //if (checkBox1.Checked)
                    //{
                    //    var json = JsonConvert.SerializeObject(tmptag);
                    //    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    //    var url = textBox_laptimeService.Text + "/laps/" + tmptag.TagId;
                    //    var client = new HttpClient();

                    //    var response = await client.PostAsync(url, data);
                    //    var result = await response.Content.ReadAsStringAsync();
                    //}

                    if (openlapsbytag.ContainsKey(tmptag.TagId))
                    {
                        Lap tmplap = openlapsbytag[tmptag.TagId];
                        tmplap.finished = tmptag.TagSeenTime;
                        if (!finishedlapsbytag.ContainsKey(tmplap.tagId))
                        {
                            List<Lap> newlaplist = new List<Lap>();
                            newlaplist.Add(tmplap);
                            finishedlapsbytag.Add(tmplap.tagId, newlaplist);
                        }
                        else
                        {
                            finishedlapsbytag[tmplap.tagId].Add(tmplap);
                            if (checkBox1.Checked)
                            {
                                sendLapToOLS(tmplap);

                            }

                        }
                        openlapsbytag.Remove(tmplap.tagId);

                        dataListView1.DataSource = new BindingSource(finishedlapsbytag, null);
                        foreach (BrightIdeasSoftware.OLVColumn column in dataListView1.AllColumns)
                        {
                            column.Groupable = false;
                            if (column.Name.Equals("Value"))
                            {
                                column.IsVisible = false;
                            }
                        }
                        dataListView1.RebuildColumns();
                        dataListView1.AutoResizeColumns();


                    }

                    Lap newlap = new Lap(tmptag.TagId, tmptag.TagSeenTime);
                    openlapsbytag.Add(newlap.tagId, newlap);


                }
            }
            else
            {
                Symbol.RFID3.TagData[] tagData = rfid3.Actions.GetReadTags(1000);
                // rausgenommen
                //objectListView1.SetObjects(tagData);
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

            private void dataListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataListView1.SelectedItem != null)
            {
                var tagid = dataListView1.SelectedItem.Text;
                objectListView2.SetObjects(finishedlapsbytag[tagid]);
                objectListView2.AutoResizeColumns();
            }
        }

        private void rfidConnectButton_Click(object sender, EventArgs e)
        {
            //string hostname = "192.168.178.100";
            string hostname = rfidReaderTextBox1.Text;

            UpdateReadHandler = new UpdateRead(myUpdateRead);

            rfid3 = new RFIDReader(hostname, 5084, 2000);

            tagsdict = new Dictionary<String, Tag>();
            openlapsbytag = new Dictionary<string, Lap>();
            finishedlapsbytag = new Dictionary<string, List<Lap>>();

            rfid3.Connect();
            toolStripStatusLabel1.Text = String.Format("FirwareVersion={0}", rfid3.ReaderCapabilities.FirwareVersion);


            triggerInfo = new TriggerInfo();
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

            // Status Events vom Reader
            rfid3.Events.StatusNotify += Events_StatusNotify;
            rfid3.Events.NotifyInventoryStartEvent = true;
            rfid3.Events.NotifyInventoryStopEvent = true;

            //rfid3.Actions.Inventory.Perform(null, triggerInfo, null);
        }

        private async void sendLapToOLS(Lap completelap)
        {
            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };
            var json = JsonConvert.SerializeObject(completelap, settings);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = textBox_laptimeService.Text + "/laps";
            var client = new HttpClient();

            var response = await client.PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();

        }

        private async void registerTokenOLS(string tokenid)
        {
            var newtag = new RegisterTag(tokenid, "manual add...");
            var json = JsonConvert.SerializeObject(newtag);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = textBox_laptimeService.Text + "/tags";
            var client = new HttpClient();

            var response = await client.PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();
        }


        public void SetInventoryStatus(bool active)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetInventoryStatus), new object[] { active });
                return;
            }
            readerInventoryActiveCheckBox.Checked = active;
        }

        private void Events_StatusNotify(object sender, Events.StatusEventArgs e)
        {
            if (e.StatusEventData.StatusEventType == Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_START_EVENT)
            {
                SetInventoryStatus(true);
            }
            else if (e.StatusEventData.StatusEventType == Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_STOP_EVENT)
            {
                SetInventoryStatus(false);
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (rfid3.IsConnected)
            {
                if (((CheckBox)sender).Checked)
                {
                    rfid3.Actions.Inventory.Perform(null, triggerInfo, null);
                }
                else
                {
                    rfid3.Actions.Inventory.Stop();
                }
            }   

        }

        private void tagRegistierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            registerTokenOLS(dataListView1.SelectedItem.Text);
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

    class RegisterTag
    {
        public RegisterTag(string tagid, string description)
        {
            this.tagid = tagid;
            this.description = description;

        }

        public string tagid { get; set; }
        public string description { get; set; }


    }

    class Lap
    {
        public Lap(string tagId, DateTime start)
        {
            this.tagId = tagId;
            this.started = start;
        }
        public string tagId { get; set; }
        public DateTime started { get; set; }
        public DateTime finished { get; set; }

        public TimeSpan laptime()
        {
            TimeSpan laptime = this.finished - this.started;
            return laptime;
        }
    }
}
