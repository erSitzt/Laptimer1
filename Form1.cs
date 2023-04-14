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
using QRCoder;
using System.IO;
using ExcelDataReader;

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
        ILiteCollection<UnfinishedLap> unfinishedLapsCollection;
        ILiteCollection<Tag> tagsCollection;

        private RadioButton[] antennas = new RadioButton[10];

        private UpdateRead UpdateReadHandler = null;

        private HttpClient HttpClient;

        double MinLaptime = 60;


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
                    var openlap = getUnfinishedLapByTagidFromDB(tmptag.TagId);
                    var openlap2 = getOpenLapFromDB(tmptag.TagId);
                    // @TODO: offene lap aus db nutzen...
                    //if (openlapsbytag.ContainsKey(tmptag.TagId))
                    if (openlap2 != null)
                    {
                        if (tagLastSeenInSeconds > MinLaptime)
                        {
                            openlap2.finished = tmptag.TagSeenTime;
                            openlap2.isCompleted = true;

                            saveCompletedLapToDB(openlap2);
                            if (checkBox1.Checked)
                            {
                                sendLapToOLS(openlap2);
                            }

                            // add new open Lap 
                            saveLapToDB(new Lap() { tagId = tmptag.TagId, started = tmptag.TagSeenTime });
                            saveUnfinishedLapToDB(new UnfinishedLap()
                            {
                                Id = tmptag.TagId,
                                started = tmptag.TagSeenTime
                            });

                            objectListView1.SetObjects(getTagsFromDB());
                            objectListView1.AutoResizeColumns();
                        }
                    }
                    if (openlap2 == null)
                    {
                        // should only run on first detection of tag
                        Lap newlap = new Lap()
                        {
                            tagId = tmptag.TagId,
                            started = tmptag.TagSeenTime
                        };
                        saveLapToDB(newlap);
                        saveUnfinishedLapToDB(new UnfinishedLap()
                        {
                            Id = tmptag.TagId,
                            started = tmptag.TagSeenTime
                        });
                    }

                    tagsdict.Remove(tmptag.TagId);
                    tagsdict.Add(tmptag.TagId, tmptag);
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



        private Lap getOpenLapFromDB(string tagid)
        {
            Lap lap;
            using (var db = new LiteDatabase(connstr))
            {
                // Get customer collection
                lapsCollection = db.GetCollection<Lap>("laps");
                lap = lapsCollection.FindOne(x => x.tagId == tagid && x.isCompleted == false);
                var res = lapsCollection.Query().Where(x => x.tagId == tagid && x.isCompleted == false);

            }
            return lap;

        }

        private List<Lap> getLapsByTagidFromDB(Tag tag)
        {
            string tagid = tag.TagId;
            addlist(String.Format("Getting Laps for : {0} from DB", tagid));
            List<Lap> laps = new List<Lap>();
            using (var db = new LiteDatabase(connstr))
            {
                // Get customer collection
                lapsCollection = db.GetCollection<Lap>("laps");
                var lapsList = lapsCollection.Find(x => x.tagId == tagid && x.isCompleted == true).ToList();
                laps = lapsList.ToList<Lap>();
            }
            var totalSpan = new TimeSpan(laps.Sum(r => r.laptime().Ticks));
            tag.totalTime = totalSpan;
            tag.lapCount = laps.Count;
            return laps;

        }

        private void saveCompletedLapToDB(Lap lap)
        {
            using (var db = new LiteDatabase(connstr))
            {
                lapsCollection = db.GetCollection<Lap>("laps");
                lapsCollection.Update(lap);
            }
            addlist(String.Format("Completed Lap for : {0} saved to DB", lap.tagId));

        }

        private void saveUnfinishedLapToDB(UnfinishedLap lap)
        {
            using (var db = new LiteDatabase(connstr))
            {
                unfinishedLapsCollection = db.GetCollection<UnfinishedLap>("unfinishedlaps");
                unfinishedLapsCollection.Upsert(lap);
            }
            addlist(String.Format("Unfinished Lap for : {0} saved to DB", lap.Id));

        }
        private UnfinishedLap getUnfinishedLapByTagidFromDB(string tagid)
        {
            addlist(String.Format("Getting unfinished Lap for : {0} from DB", tagid));
            UnfinishedLap lap;
            using (var db = new LiteDatabase(connstr))
            {
                // Get customer collection
                unfinishedLapsCollection = db.GetCollection<UnfinishedLap>("unfinishedlaps");
                lap = unfinishedLapsCollection.FindOne(x => x.Id == tagid);

            }
            return lap;

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

        private async Task<HttpResponseMessage> sendLapToOLSTask(Lap completelap)
        {
            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };
            var json = JsonConvert.SerializeObject(completelap, settings);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = textBox_laptimeService.Text + "/laps";

            // @todo try catch
            var response = await HttpClient.PostAsync(url, data);
            return response;

        }

        private async void registerTagOLS(string tagid)
        {
            var newtag = new RegisterTag(tagid, "manual add...");
            var json = JsonConvert.SerializeObject(newtag);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = textBox_laptimeService.Text + "/tags";


            var response = await HttpClient.PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> registerTagOLSTask(string tagid)
        {
            var newtag = new RegisterTag(tagid, "manual add...");
            var json = JsonConvert.SerializeObject(newtag);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = textBox_laptimeService.Text + "/tags";


            var response = await HttpClient.PostAsync(url, data);
            return response;
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
            registerTagOLS(objectListView1.SelectedItem.Text);
        }

        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objectListView1.SelectedItem != null)
            {



                Tag selectedTag = (Tag)objectListView1.SelectedObject;
                objectListView2.SetObjects(getLapsByTagidFromDB(selectedTag));
                objectListView2.AutoResizeColumns();
                List<UnfinishedLap> tmplist = new List<UnfinishedLap>();
                UnfinishedLap ulap = getUnfinishedLapByTagidFromDB(selectedTag.TagId);
                if (ulap != null)
                {
                    tmplist.Add(ulap);
                    fastObjectListView1.SetObjects(tmplist);
                    fastObjectListView1.AutoResizeColumns();
                }

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                String url = String.Format("http://openlaptime.de/{0}", selectedTag.TagId);
                QRCodeData qrCodeData = qrGenerator.CreateQrCode( url, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(4);
                pictureBox1.Image = qrCodeImage;


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
            Setting settings = new Setting() { id = 1, apikey = textBox1.Text };
            using (var db = new LiteDatabase(connstr))
            {
                var settingsCollection = db.GetCollection<Setting>("settings");
                settingsCollection.Upsert(settings);
            }
            addlist(String.Format("Lap for : {0} saved to DB", settings.apikey));

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            MinLaptime = ((double)numericUpDown1.Value);
            Console.WriteLine(MinLaptime);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Tag> tagsFromDB = getTagsFromDB();
            
            foreach (Tag tag in tagsFromDB)
            {
                Task<HttpResponseMessage> registerTask = Task.Run<HttpResponseMessage>(async () => await registerTagOLSTask(tag.TagId));
                String tagRegisterStatusCode = registerTask.Result.StatusCode.ToString();
                addlist(String.Format("Tag {0} registriert. Status Code : {1}", tag.TagId, tagRegisterStatusCode));
                //registerTagOLS(tag.TagId);
                List<Lap> laps = getLapsByTagidFromDB(tag);
                foreach (Lap lap in laps)
                {
                    Task<HttpResponseMessage> sendlapTask = Task.Run<HttpResponseMessage>(async () => await sendLapToOLSTask(lap));
                    String sendLapStatusCode = sendlapTask.Result.StatusCode.ToString();
                    //sendLapToOLS(lap);
                    addlist(String.Format("Lap {0} to {1} - Status Code : {2}", lap.started, lap.finished, sendLapStatusCode));
                    //sendLapToOLS(lap);
                    //addlist(String.Format("Lap {0} to {1}", lap.started, lap.finished));
                }
            }
        }
        private void registerTag(Tag tag)
        {
            Task<HttpResponseMessage> registerTask = Task.Run<HttpResponseMessage>(async () => await registerTagOLSTask(tag.TagId));
            String tagRegisterStatusCode = registerTask.Result.StatusCode.ToString();
            addlist(String.Format("Tag {0} registriert. Status Code : {1}", tag.TagId, tagRegisterStatusCode));
        }

        private void sendLap(Lap lap)
        {
            Task<HttpResponseMessage> sendlapTask = Task.Run<HttpResponseMessage>(async () => await sendLapToOLSTask(lap));
            String sendLapStatusCode = sendlapTask.Result.StatusCode.ToString();
            addlist(String.Format("Lap {0} to {1} - Status Code : {2}", lap.started, lap.finished, sendLapStatusCode));
        }

        private void lapsÜbertragenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tag tag = (Tag)objectListView1.SelectedObject;
            registerTag(tag);

            foreach (Lap lap in getLapsByTagidFromDB(tag))
            {
                sendLap(lap);
            }
            
        }

        private void alleTagsRegistrierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(Tag tag in objectListView1.SelectedObjects)
            {
                registerTag(tag);
            }
        }

        private void lapsFürAusgewTagsÜbertragenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Tag tag in objectListView1.SelectedObjects)
            {
                registerTag(tag);
                foreach (Lap lap in getLapsByTagidFromDB(tag))
                {
                    sendLap(lap);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Tag tmptag = new Tag();
            tmptag.TagId = "99999";
            if (!tagsdict.ContainsKey(tmptag.TagId))
            {
                tagsdict.Add(tmptag.TagId, tmptag);
                saveTagToDB(tmptag);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Excel file (*.xlsx)|*.xlsx";
            openFileDialog1.FileName = "";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true,
                                FilterRow = (rowReader) => {
                                    int progress = (int)Math.Ceiling((decimal)rowReader.Depth / (decimal)rowReader.RowCount * (decimal)100);
                                    // progress is in the range 0..100
                                    return true;
                                }
                            }
                        });
                        dataGridView1.DataSource = result.Tables[0];
                    }
                }
            }
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

        public TimeSpan totalTime { get; set; }

        public int lapCount { get; set; }


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

    class UnfinishedLap
    {

        public string Id { get; set; }
        public DateTime started { get; set; }
        public DateTime finished { get; set; }

    }

    class Lap
    {
        public int Id { get; set; }
        public string tagId { get; set; }
        public DateTime started { get; set; }
        public DateTime finished { get; set; }

        public bool isCompleted { get; set; }

        public TimeSpan laptime()
        {
            TimeSpan laptime = this.finished - this.started;
            return laptime;
        }
    }
}
