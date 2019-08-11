using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pili_sdk_csharp.pili;
using pili_sdk_csharp.pili_qiniu;

namespace UnitTest
{
    [TestClass]
    public class StreamTester : TestEntry
    {
        //测试创建直播流所用的临时流名称
        public string testStreamName = "weishakeji";

        /// <summary>
        /// 创建流
        /// </summary>
        [TestMethod]
        public void Hub_CreatStream()
        {

            Hub hub = Hub.Create(ACCESS_KEY, SECRET_KEY, HUB_NAME);
            //创建流
            string title = testStreamName; // optional, auto-generated as default
            string publishKey = null; // optional, auto-generated as default
            string publishSecurity = null; // optional, can be "dynamic" or "static", "dynamic" as default
            Stream stream = null;
            try
            {
                stream = hub.createStream(title, publishKey, publishSecurity);
                //Trace.WriteLine("hub.createStream:");
                Console.WriteLine(stream.toJsonString());
            }
            catch (PiliException e)
            {
                Assert.Fail();
            }
        }
        /// <summary>
        /// 删除直播流
        /// </summary>
        [TestMethod]
        public void Stream_Delete()
        {
            Hub hub = Hub.Create(ACCESS_KEY, SECRET_KEY, HUB_NAME);
            Stream stream = null;
            try
            {
                stream = hub.getStream("z1." + HUB_NAME + "."+ testStreamName);
                string json = stream.toJsonString();
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            if (stream != null)
            {
                try
                {
                    string res = stream.delete();
                    Console.WriteLine("Stream delete()");
                    Console.WriteLine(res);
                    // No Content
                }
                catch (PiliException e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }
            else
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Stream_saveas()
        {
            Hub hub = Hub.Create(ACCESS_KEY, SECRET_KEY, HUB_NAME);
            Stream stream = null;           

            string saveAsFormat = "mp4"; // required
            string saveAsName = "videoName" + "." + saveAsFormat; // required
            //long saveAsStart = 1444897613; // required, in second, unix timestampstart:1444897613,end:1444897973
            //long saveAsEnd = 1444897973; // required, in second, unix timestamp
            //string saveAsNotifyUrl = null; // optional
            try
            {
                stream = hub.getStream("z1." + HUB_NAME + "." + testStreamName);
                Stream.Status status = stream.status();

                Stream.SaveAsResponse response = stream.saveAs(saveAsName, saveAsFormat);
                Console.WriteLine("Stream saveAs()");
                Console.WriteLine(response.ToString());
                /*
				 {
				     "url":"http://ey636h.vod1.z1.pili.qiniucdn.com/recordings/z1.test-hub.55d81a72e3ba5723280000ec/videoName.m3u8",
				     "targetUrl":"http://ey636h.vod1.z1.pili.qiniucdn.com/recordings/z1.test-hub.55d81a72e3ba5723280000ec/videoName.mp4",
				     "persistentId":"z1.55d81c6c7823de5a49ad77b3"
				 }
				*/
            }
            catch (PiliException e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Sream_Snapshot()
        {
            Hub hub = Hub.Create(ACCESS_KEY, SECRET_KEY, HUB_NAME);
            Stream stream = null;

            string format = "jpg"; // required
            string name = "imageName" + "." + format; // required
            long time = 0; // optional, in second, unix timestamp
            string notifyUrl = null; // optional

            try
            {
                stream = hub.getStream("z1." + HUB_NAME + "." + testStreamName);
                Stream.SnapshotResponse response = stream.snapshot(name, format, time, notifyUrl);
                Console.WriteLine("Stream snapshot()");
                Console.WriteLine(response.ToString());
                /*
                 {
                     "targetUrl":"http://ey636h.static1.z0.pili.qiniucdn.com/snapshots/z1.test-hub.55d81a72e3ba5723280000ec/imageName.jpg",
                     "persistentId":"z1.55d81c247823de5a49ad729c"
                 }
                 */
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }
        /// <summary>
        /// 获取直播流信息
        /// </summary>
        [TestMethod]
        public void Hub_GetStream()
        {
            Hub hub = Hub.Create(ACCESS_KEY, SECRET_KEY, HUB_NAME);
            Stream stream = null;
            try
            {
                stream = hub.getStream("z1."+HUB_NAME+ "."+ testStreamName);
                string json = stream.toJsonString();
                /*
                     {
                          "id": "z1.liuhanlin.562f2b35d409d2aa48001102",
                          "createdAt": "2015-10-27T07:43:49.756Z",
                          "updatedAt": "2015-10-27T07:43:49.756Z",
                          "title": "562f2b35d409d2aa48001102",
                          "hub": "liuhanlin",
                          "disabled": false,
                          "publishKey": "9654c11a01b7b941",
                          "publishSecurity": "static",
                          "hosts": {
                            "publish": {
                              "rtmp": "100000p.publish.z1.pili.qiniup.com"
                            },
                            "live": {
                              "hdl": "100000p.live1-hdl.z1.pili.qiniucdn.com",
                              "hls": "100000p.live1-hls.z1.pili.qiniucdn.com",
                              "http": "100000p.live1-hls.z1.pili.qiniucdn.com",
                              "rtmp": "100000p.live1-rtmp.z1.pili.qiniucdn.com"
                            },
                            "playback": {
                              "hls": "100000p.playback1.z1.pili.qiniucdn.com",
                              "http": "100000p.playback1.z1.pili.qiniucdn.com"
                            },
                            "play": {
                              "http": "100000p.live1-hls.z1.pili.qiniucdn.com",
                              "rtmp": "100000p.live1-rtmp.z1.pili.qiniucdn.com"
                            }
                          }
                        }
                     */
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }
        [TestMethod]
        public void Hub_GetStream2()
        {
            Hub hub = Hub.Create(ACCESS_KEY, SECRET_KEY, HUB_NAME);
            Stream stream = null;
            try
            {
                string marker = null; // optional
                long limit = 2; // optional
                string titlePrefix = null; // optional
                Stream.StreamList streamList = hub.listStreams(marker, limit, titlePrefix);
                while (stream == null && streamList.Streams.Count > 0)
                {
                    //streamList = hub.listStreams(marker, limit, titlePrefix);
                    IList<Stream> list = streamList.Streams;
                    foreach (Stream s in list)
                    {
                        if (s.StreamId.EndsWith("." + HUB_NAME + "." + testStreamName))
                        {
                            stream = s;
                            break;
                        }
                    }
                    if (stream == null)
                    {
                        streamList = hub.listStreams(streamList.Marker, limit, titlePrefix);
                    }
                }

            }
            catch (PiliException e)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Hub_GetStreamActivity()
        {
            Hub hub = Hub.Create(ACCESS_KEY, SECRET_KEY, HUB_NAME);
            List<Stream> list = new List<Stream>();
            try
            {
                string marker = null; // optional
                long limit = 0; // optional
                string titlePrefix = null; // optional
                Stream.StreamList streamList = hub.listStreams(marker, limit, titlePrefix,null);
                IList<Stream> streams = streamList.Streams;
                foreach (Stream s in streams)
                {
                    Stream.Status status = s.status();
                    
                }
            }
            catch (PiliException e)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void _CreatStream()
        {
            Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
            Hub hub = new Hub(credentials, HUB_NAME);
            Stream stream = null;
            try
            {
                stream = hub.getStream(stream.StreamId);
                Console.WriteLine("hub.getStream:");
                Console.WriteLine(stream.toJsonString());
                /*
                {
                    "id":"z1.test-hub.55d80075e3ba5723280000d2",
                    "createdAt":"2015-08-22T04:54:13.539Z",
                    "updatedAt":"2015-08-22T04:54:13.539Z",
                    "title":"55d80075e3ba5723280000d2",
                    "hub":"test-hub",
                    "disabled":false,
                    "publishKey":"ca11e07f094c3a6e",
                    "publishSecurity":"dynamic",
                    "hosts":{
                        "publish":{
                            "rtmp":"ey636h.publish.z1.pili.qiniup.com"
                         },
                         "live":{
                             "http":"ey636h.live1-http.z1.pili.qiniucdn.com",
                             "rtmp":"ey636h.live1-rtmp.z1.pili.qiniucdn.com"
                         },
                         "playback":{
                             "http":"ey636h.playback1.z1.pili.qiniucdn.com"
                         }
                     }
                 }
                 */
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            // List streams
            try
            {
                string marker = null; // optional
                long limit = 0; // optional
                string titlePrefix = null; // optional

                Stream.StreamList streamList = hub.listStreams(marker, limit, titlePrefix);
                Console.WriteLine("hub.listStreams()");
                Console.WriteLine("marker:" + streamList.Marker);
                IList<Stream> list = streamList.Streams;
                foreach (Stream s in list)
                {
                    // access the stream
                }

                /*
                 marker:10
                 stream object
                 */
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            //////////////////////////////////////////////////////////////////////////////////////////
            // Hub end
            //////////////////////////////////////////////////////////////////////////////////////////

            //////////////////////////////////////////////////////////////////////////////////////////
            // Stream begin
            //////////////////////////////////////////////////////////////////////////////////////////
            string streamJsonString = stream.toJsonString();
            Console.WriteLine("Stream toJSONString()");
            Console.WriteLine(streamJsonString);

            /*
                {
                    "id":"z1.test-hub.55d80075e3ba5723280000d2",
                    "createdAt":"2015-08-22T04:54:13.539Z",
                    "updatedAt":"2015-08-22T04:54:13.539Z",
                    "title":"55d80075e3ba5723280000d2",
                    "hub":"test-hub",
                    "disabled":false,
                    "publishKey":"ca11e07f094c3a6e",
                    "publishSecurity":"dynamic",
                    "hosts":{
                        "publish":{
                            "rtmp":"ey636h.publish.z1.pili.qiniup.com"
                         },
                         "live":{
                             "http":"ey636h.live1-http.z1.pili.qiniucdn.com",
                             "rtmp":"ey636h.live1-rtmp.z1.pili.qiniucdn.com"
                         },
                         "playback":{
                             "http":"ey636h.playback1.z1.pili.qiniucdn.com"
                         }
                     }
                 }
             */

            // Update a Stream
            string newPublishKey = "new_secret_words"; // optional
            string newPublishSecurity = "static"; // optional, can be "dynamic" or "static"
            bool newDisabled = true; // optional, can be "true" of "false"
            try
            {
                Stream newStream = stream.update(newPublishKey, newPublishSecurity, newDisabled);
                Console.WriteLine("Stream update()");
                Console.WriteLine(newStream.toJsonString());
                stream = newStream;
                /*
                {
                    "id":"z1.test-hub.55d80075e3ba5723280000d2",
                    "createdAt":"2015-08-22T04:54:13.539Z",
                    "updatedAt":"2015-08-22T01:53:02.738973745-04:00",
                    "title":"55d80075e3ba5723280000d2",
                    "hub":"test-hub",
                    "disabled":true,
                    "publishKey":"new_secret_words",
                    "publishSecurity":"static",
                    "hosts":{
                        "publish":{
                            "rtmp":"ey636h.publish.z1.pili.qiniup.com"
                         },
                         "live":{
                             "http":"ey636h.live1-http.z1.pili.qiniucdn.com",
                             "rtmp":"ey636h.live1-rtmp.z1.pili.qiniucdn.com"
                         },
                         "playback":{
                             "http":"ey636h.hls.z1.pili.qiniucdn.com"
                         }
                     }
                 }
             */
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }

            // Disable a Stream
            try
            {
                Stream disabledStream = stream.disable();
                Console.WriteLine("Stream disable()");
                Console.WriteLine(disabledStream.Disabled);
                /*
                 * true
                 * 
                 * */
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            // Enable a Stream
            try
            {
                Stream enabledStream = stream.enable();
                Console.WriteLine("Stream enable()");
                Console.WriteLine(enabledStream.Disabled);
                /*
                 * false
                 * 
                 * */
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            // Get Stream status
            try
            {
                Stream.Status status = stream.status();
                Console.WriteLine("Stream status()");
                Console.WriteLine(status.ToString());
                /*
                {
                    "addr":"222.73.202.226:2572",
                    "status":"disconnected",
                    "bytesPerSecond":0,
                    "framesPerSecond":{
                        "audio":0,
                        "video":0,
                        "data":0
                     }
                 }
                */
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }

            // Generate RTMP publish URL
            try
            {
                string publishUrl = stream.rtmpPublishUrl();
                Console.WriteLine("Stream rtmpPublishUrl()");
                Console.WriteLine(publishUrl);
                // rtmp://ey636h.publish.z1.pili.qiniup.com/test-hub/55d810aae3ba5723280000db?nonce=1440223404&token=hIVJje0ZOX9hp7yPIvGBmJ_6Qxo=

            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            // Generate RTMP live play URLs
            string originUrl = (stream.rtmpLiveUrls())[Stream.ORIGIN];
            Console.WriteLine("Stream rtmpLiveUrls()");
            Console.WriteLine(originUrl);
            // rtmp://ey636h.live1-rtmp.z1.pili.qiniucdn.com/test-hub/55d8113ee3ba5723280000dc

            // Generate HLS play URLs
            string originLiveHlsUrl = stream.hlsLiveUrls()[Stream.ORIGIN];
            Console.WriteLine("Stream hlsLiveUrls()");
            Console.WriteLine(originLiveHlsUrl);
            // http://ey636h.live1-http.z1.pili.qiniucdn.com/test-hub/55d8119ee3ba5723280000dd.m3u8

            // Generate Http-Flv live play URLs
            string originLiveFlvUrl = stream.httpFlvLiveUrls()[Stream.ORIGIN];
            Console.WriteLine("Stream httpFlvLiveUrls()");
            Console.WriteLine(originLiveFlvUrl);
            // http://ey636h.live1-http.z1.pili.qiniucdn.com/test-hub/55d8119ee3ba5723280000dd.flv




            // Generate HLS playback URLs
            long startHlsPlayback = 1440315411; // required, in second, unix timestamp
            long endHlsPlayback = 1440315435; // required, in second, unix timestamp
            try
            {
                string hlsPlaybackUrl = stream.hlsPlaybackUrls(startHlsPlayback, endHlsPlayback)[Stream.ORIGIN];

                Console.WriteLine("Stream hlsPlaybackUrls()");
                Console.WriteLine(hlsPlaybackUrl);
                // http://ey636h.playback1.z1.pili.qiniucdn.com/test-hub/55d8119ee3ba5723280000dd.m3u8?start=1440315411&end=1440315435
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }



            // Get Stream status
            try
            {
                Stream.Status status = stream.status();
                Console.WriteLine("Stream status()");
                Console.WriteLine(status.ToString());
                /*
                {
                    "addr":"222.73.202.226:2572",
                    "status":"disconnected",
                    "bytesPerSecond":0,
                    "framesPerSecond":{
                        "audio":0,
                        "video":0,
                        "data":0
                     }
                 }
                */
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }



            // Delete a Stream
            try
            {
                string res = stream.delete();
                Console.WriteLine("Stream delete()");
                Console.WriteLine(res);
                // No Content
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            //////////////////////////////////////////////////////////////////////////////////////////
            // Stream end

            try
            {
                stream = hub.getStream(stream.StreamId);
                Console.WriteLine("hub.getStream:");
                Console.WriteLine(stream.toJsonString());
                /*
                {
                    "id":"z1.test-hub.55d80075e3ba5723280000d2",
                    "createdAt":"2015-08-22T04:54:13.539Z",
                    "updatedAt":"2015-08-22T04:54:13.539Z",
                    "title":"55d80075e3ba5723280000d2",
                    "hub":"test-hub",
                    "disabled":false,
                    "publishKey":"ca11e07f094c3a6e",
                    "publishSecurity":"dynamic",
                    "hosts":{
                        "publish":{
                            "rtmp":"ey636h.publish.z1.pili.qiniup.com"
                         },
                         "live":{
                             "http":"ey636h.live1-http.z1.pili.qiniucdn.com",
                             "rtmp":"ey636h.live1-rtmp.z1.pili.qiniucdn.com"
                         },
                         "playback":{
                             "http":"ey636h.playback1.z1.pili.qiniucdn.com"
                         }
                     }
                 }
                 */
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }

        /// <summary>
        /// 获取流的列表
        /// </summary>
        [TestMethod]
        public void Hub_ListStreams()
        {
            Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
            Hub hub = new Hub(credentials, HUB_NAME);
            try
            {
                string marker = null; // optional
                long limit = 0; // optional
                string titlePrefix = null; // optional

                Stream.StreamList streamList = hub.listStreams(marker, limit, titlePrefix);
                Console.WriteLine("hub.listStreams()");
                Console.WriteLine("marker:" + streamList.Marker);
                IList<Stream> list = streamList.Streams;
                foreach (Stream s in list)
                {
                    // access the stream
                }
            }
            catch (PiliException e)
            {
                Assert.Fail();
                throw e;
            }
        }
        [TestMethod]
        public void Hub_ListStreamsActivity()
        {
            Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
            Hub hub = new Hub(credentials, HUB_NAME);
            try
            {
                string marker = null; // optional
                long limit = 0; // optional
                string titlePrefix = null; // optional

                Stream.StreamList streamList = hub.listStreams(marker, limit, titlePrefix,true);
                Console.WriteLine("hub.listStreams()");
                Console.WriteLine("marker:" + streamList.Marker);
                IList<Stream> list = streamList.Streams;
                foreach (Stream s in list)
                {
                    // access the stream
                }
            }
            catch (PiliException e)
            {
                Assert.Fail();
                throw e;
            }
        }
    }
}
