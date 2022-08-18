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
using LiteDB;
using BrightIdeasSoftware;

namespace Laptimer1
{
    public partial class Form1 : Form
    {
        RFIDReader rfid3;
        TriggerInfo triggerInfo;

        Dictionary<String, Tag> tagsdict;

        Dictionary<String, Lap> openlapsbytag;
        Dictionary<String, List<Lap>> finishedlapsbytag;

        ConnectionString connstr;
        ILiteCollection<Lap> lapsCollection;
        ILiteCollection<Tag> tagsCollection;

        private RadioButton[] antennas = new RadioButton[10];

        private UpdateRead UpdateReadHandler = null;

        private HttpClient HttpClient;


        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // init dicts
            tagsdict = new Dictionary<String, Tag>();
            openlapsbytag = new Dictionary<string, Lap>();
            finishedlapsbytag = new Dictionary<string, List<Lap>>();

            // LiteDB 
            connstr = new ConnectionString(@"laptime.db")
            {
                Connection = ConnectionType.Shared
            };
            objectListView1.SetObjects(getTagsFromDB());
            objectListView1.AutoResizeColumns();


            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Add("X-Api-Key", "Start123");

        }
        private delegate void Update();
        public void addlist(string item)
        {
            listBox2.Invoke(new Update(() => listBox2.Items.Add(item)));
            listBox2.Invoke(new Update(() => listBox2.SelectedIndex = listBox2.Items.Count - 1));
            listBox2.Invoke(new Update(() => listBox2.SelectedIndex = -1));
        }
        private delegate void UpdateRead(Events.ReadEventData eventData);
        private void myUpdateRead(Events.ReadEventData eventData)
        {
            if (rfid3.Events.AttachTagDataWithReadEvent)
            {
                //await Task.Run(() => HandleTags(eventData.TagData));

                addlist(String.Format("TagID : {0} : {1} : {2} : {3}", eventData.TagData.TagID, eventData.TagData.AntennaID, eventData.TagData.TagEvent, eventData.TagData.TagEventTimeStamp));
                Tag tmptag = new Tag()
                {
                    TagId = eventData.TagData.TagID,
                    TagSeenTime = eventData.TagData.TagEventTimeStamp.ToLocalTime()
                };


                if (!tagsdict.ContainsKey(tmptag.TagId))
                {
                    tagsdict.Add(tmptag.TagId, tmptag);
                    saveTagToDB(tmptag);
                }
                if (eventData.TagData.OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ && eventData.TagData.OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS)
                {
                    addlist(String.Format("{0} : {1}", eventData.TagData.MemoryBank.ToString(), eventData.TagData.MemoryBankData));

                }
                if (eventData.TagData.TagEvent == TAG_EVENT.NEW_TAG_VISIBLE || eventData.TagData.TagEvent == TAG_EVENT.TAG_BACK_TO_VISIBILITY)
                {
                    double tagLastSeenInSeconds = (tmptag.TagSeenTime - tagsdict[tmptag.TagId].TagSeenTime).TotalSeconds;
                    addlist(String.Format("TagID : {0} last seen {1:F2} seconds ago", eventData.TagData.TagID, tagLastSeenInSeconds));

                    if (openlapsbytag.ContainsKey(tmptag.TagId))
                    {
                        if (tagLastSeenInSeconds > 10.0)
                        {
                            Lap tmplap = openlapsbytag[tmptag.TagId];
                            tmplap.finished = tmptag.TagSeenTime;
                            if (!finishedlapsbytag.ContainsKey(tmplap.tagId))
                            {
                                List<Lap> newlaplist = new List<Lap>();
                                newlaplist.Add(tmplap);
                                finishedlapsbytag.Add(tmplap.tagId, newlaplist);
                                saveLapToDB(tmplap);
                            }
                            else
                            {
                                finishedlapsbytag[tmplap.tagId].Add(tmplap);
                                saveLapToDB(tmplap);
                                if (checkBox1.Checked)
                                {
                                    sendLapToOLS(tmplap);
                                }

                            }
                            openlapsbytag.Remove(tmplap.tagId);

                            objectListView1.SetObjects(getTagsFromDB());
                            objectListView1.AutoResizeColumns();
                        }
                    }
                    if (!openlapsbytag.ContainsKey(tmptag.TagId))
                    {
                        Lap newlap = new Lap()
                        {
                            tagId = tmptag.TagId,
                            started = tmptag.TagSeenTime
                        };
                        openlapsbytag.Add(newlap.tagId, newlap);
                    }


                }
            }
            else
            {
                Symbol.RFID3.TagData[] tagData = rfid3.Actions.GetReadTags(1000);
                // rausgenommen
                //objectListView1.SetObjects(tagData);
            }
        }

        void HandleTags(TagData tagdata)
        {
            addlist(String.Format("TagID : {0} : {1} : {2} : {3} : {4}", tagdata.TagID, tagdata.AntennaID, tagdata.TagEvent, tagdata.tagTransitionStatus, tagdata.TagEventTimeStamp));
            Tag tmptag = new Tag()
            {
                TagId = tagdata.TagID,
                TagSeenTime = tagdata.TagEventTimeStamp
            };
            tagsdict.Add(tmptag.TagId, tmptag);
            if (!tagsdict.ContainsKey(tmptag.TagId))
            {
                saveTagToDB(tmptag);
            }
            if (tagdata.OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ && tagdata.OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS)
            {
                addlist(String.Format("{0} : {1}", tagdata.MemoryBank.ToString(), tagdata.MemoryBankData));

            }
            if (tagdata.TagEvent == TAG_EVENT.NEW_TAG_VISIBLE || tagdata.TagEvent == TAG_EVENT.TAG_BACK_TO_VISIBILITY)
            {

                if (openlapsbytag.ContainsKey(tmptag.TagId))
                {
                    Lap tmplap = openlapsbytag[tmptag.TagId];
                    tmplap.finished = tmptag.TagSeenTime;
                    if (!finishedlapsbytag.ContainsKey(tmplap.tagId))
                    {
                        List<Lap> newlaplist = new List<Lap>();
                        newlaplist.Add(tmplap);
                        finishedlapsbytag.Add(tmplap.tagId, newlaplist);
                        saveLapToDB(tmplap);
                    }
                    else
                    {
                        finishedlapsbytag[tmplap.tagId].Add(tmplap);
                        saveLapToDB(tmplap);
                        if (checkBox1.Checked)
                        {
                            sendLapToOLS(tmplap);

                        }
                    }
                    openlapsbytag.Remove(tmplap.tagId);

                    objectListView1.SetObjects(getTagsFromDB());
                    //objectListView1.AutoResizeColumns();

                }
                Lap newlap = new Lap()
                {
                    tagId = tmptag.TagId,
                    started = tmptag.TagSeenTime
                };
                openlapsbytag.Add(newlap.tagId, newlap);
            }

        }



        private List<Lap> getLapsFromDB()
        {
            List<Lap> laps = new List<Lap>();
            using (var db = new LiteDatabase(connstr))
            {
                // Get customer collection
                lapsCollection = db.GetCollection<Lap>("laps");
                var lapsList = lapsCollection.FindAll().ToList();
                laps = lapsList.ToList<Lap>();


            }
            return laps;

        }

        private List<Lap> getLapsByTagidFromDB(string tagid)
        {
            addlist(String.Format("Getting Laps for : {0} from DB", tagid));
            List<Lap> laps = new List<Lap>();
            using (var db = new LiteDatabase(connstr))
            {
                // Get customer collection
                lapsCollection = db.GetCollection<Lap>("laps");
                var lapsList = lapsCollection.Find(x => x.tagId == tagid).ToList();
                laps = lapsList.ToList<Lap>();
            }
            return laps;

        }

        private List<Tag> getTagsFromDB()
        {
            addlist(String.Format("Getting Tags from DB"));
            List<Tag> tags = new List<Tag>();
            using (var db = new LiteDatabase(connstr))
            {
                // Get customer collection
                tagsCollection = db.GetCollection<Tag>("tags");
                var tagsList = tagsCollection.FindAll().ToList();
                tags = tagsList.ToList<Tag>();
            }
            return tags;

        }

        private void saveLapToDB(Lap lap)
        {
            using (var db = new LiteDatabase(connstr))
            {
                lapsCollection = db.GetCollection<Lap>("laps");
                lapsCollection.Insert(lap);
            }
            addlist(String.Format("Lap for : {0} saved to DB", lap.tagId));

        }

        private void saveTagToDB(Tag tag)
        {
            using (var db = new LiteDatabase(connstr))
            {
                var col = db.GetCollection<Tag>("tags");
                //col.Insert(tag);
                // test upsert
                col.Upsert(tag);
            }
            addlist(String.Format("Tag {0} saved to DB", tag.TagId));
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

        private void rfidConnectButton_Click(object sender, EventArgs e)
        {
            //string hostname = "192.168.178.100";
            string hostname = rfidReaderTextBox1.Text;

            UpdateReadHandler = new UpdateRead(myUpdateRead);

            rfid3 = new RFIDReader(hostname, 5084, 2000);

            rfid3.Connect();
            toolStripStatusLabel1.Text = String.Format("FirwareVersion={0}", rfid3.ReaderCapabilities.FirwareVersion);


            triggerInfo = new TriggerInfo();
            triggerInfo.EnableTagEventReport = true;
            triggerInfo.TagEventReportInfo.ReportNewTagEvent = TAG_EVENT_REPORT_TRIGGER.MODERATED;
            triggerInfo.TagEventReportInfo.ReportTagInvisibleEvent = TAG_EVENT_REPORT_TRIGGER.MODERATED;
            triggerInfo.TagEventReportInfo.ReportTagBackToVisibilityEvent = TAG_EVENT_REPORT_TRIGGER.MODERATED;
            triggerInfo.TagEventReportInfo.NewTagEventModeratedTimeoutMilliseconds = 500;
            triggerInfo.TagEventReportInfo.TagInvisibleEventModeratedTimeoutMilliseconds = 3000;
            triggerInfo.TagEventReportInfo.TagBackToVisibilityModeratedTimeoutMilliseconds = 500;

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

            // @todo try catch
            var response = await HttpClient.PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();

        }

        private async void registerTokenOLS(string tokenid)
        {
            var newtag = new RegisterTag(tokenid, "manual add...");
            var json = JsonConvert.SerializeObject(newtag);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = textBox_laptimeService.Text + "/tags";


            var response = await HttpClient.PostAsync(url, data);
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
            registerTokenOLS(objectListView1.SelectedItem.Text);
        }

        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objectListView1.SelectedItem != null)
            {
                var tagid = objectListView1.SelectedItem.Text;
                objectListView2.SetObjects(getLapsByTagidFromDB(tagid));
                objectListView2.AutoResizeColumns();
            }

        }

        private void resetDatabaseButton_Click(object sender, EventArgs e)
        {
            tagsdict.Clear();
            openlapsbytag.Clear();
            finishedlapsbytag.Clear();

            using (var db = new LiteDatabase(connstr))
            {
                db.DropCollection("laps");
                db.DropCollection("tags");
            }
            objectListView1.Items.Clear();
            objectListView2.Items.Clear();
            listBox2.Items.Clear();
        }

        private void objectListView1_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            Tag tag = (Tag)e.Model;
            if (openlapsbytag.ContainsKey(tag.TagId))
            {
                e.Item.BackColor = Color.LightYellow;
            }
            else if (finishedlapsbytag.ContainsKey(tag.TagId))
            {
                e.Item.BackColor = Color.LightGreen;
            }

        }

        private void objectListView1_ItemsChanged(object sender, ItemsChangedEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // settings einfach mit id=1 speichern... dumm aber egal
            Setting settings = new Setting() { id=1, apikey = textBox1.Text };
            using (var db = new LiteDatabase(connstr))
            {
                var settingsCollection = db.GetCollection<Setting>("settings");
                settingsCollection.Upsert(settings);
            }
            addlist(String.Format("Lap for : {0} saved to DB", settings.apikey));

        }
    }

    class Setting
    {
        public int id { get; set; }
        public string apikey { get; set; }

    }
    class Tag
    {

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
