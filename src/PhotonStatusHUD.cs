using System.Collections;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;
using TMPro;
using UnityEngine;

namespace TFS_PhotonNetworkChecker
{
    internal class PhotonStatusHUD : MonoBehaviour
    {
        private static readonly (string key, string host)[] Servers = new[]
        {
            ("ns", "ns.photonengine.io"),
            ("eu", "ns-eu.photonengine.io"),
            ("us", "ns-us.photonengine.io"),
            ("jp", "ns-jp.photonengine.io"),
        };

        // [server][check]: 0=IP, 1=HTTP, 2=WSS
        private bool?[,] statuses;
        private TextMeshProUGUI label;
        private bool checking = false;

        private static readonly string[] CheckLabels = { "IP", "HTTP", "WSS" };

        private void Awake()
        {
            label = GetComponent<TextMeshProUGUI>();
            statuses = new bool?[Servers.Length, 3];
            label.fontSize *= 0.75f;
        }

        private void Start()
        {
            UpdateText();
            StartCoroutine(CheckLoop());
        }

        private IEnumerator CheckLoop()
        {
            while (true)
            {
                if (!checking)
                    yield return StartCoroutine(CheckAll());
                yield return new WaitForSeconds(30f);
            }
        }

        private IEnumerator CheckAll()
        {
            checking = true;

            for (int i = 0; i < Servers.Length; i++)
                for (int j = 0; j < 3; j++)
                    statuses[i, j] = null;
            UpdateText();

            for (int i = 0; i < Servers.Length; i++)
            {
                string host = Servers[i].host;
                bool[] results = new bool[3];
                bool[] done = new bool[3];

                // Run all 3 checks in parallel for this server
                SpawnCheck(0, () => CheckIP(host), results, done);
                SpawnCheck(1, () => CheckHTTP(host), results, done);
                SpawnCheck(2, () => CheckWSS(host), results, done);

                float waited = 0f;
                while ((!done[0] || !done[1] || !done[2]) && waited < 8f)
                {
                    waited += Time.unscaledDeltaTime;
                    yield return null;
                }

                for (int j = 0; j < 3; j++)
                    statuses[i, j] = done[j] ? results[j] : false;

                UpdateText();
            }

            checking = false;
        }

        private static void SpawnCheck(int idx, System.Func<bool> check, bool[] results, bool[] done)
        {
            var thread = new Thread(() =>
            {
                try { results[idx] = check(); }
                catch { results[idx] = false; }
                finally { done[idx] = true; }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        // ── Checks ────────────────────────────────────────────────────────────

        private static bool CheckIP(string host)
        {
            var addrs = Dns.GetHostAddresses(host);
            return addrs != null && addrs.Length > 0;
        }

        private static bool CheckHTTP(string host)
        {
            // TCP connect to port 80
            using (var client = new TcpClient())
            {
                var ar = client.BeginConnect(host, 80, null, null);
                if (!ar.AsyncWaitHandle.WaitOne(3000)) return false;
                client.EndConnect(ar);
                return client.Connected;
            }
        }

        private static bool CheckWSS(string host)
        {
            // TLS connect to port 443 (WSS)
            using (var client = new TcpClient())
            {
                var ar = client.BeginConnect(host, 443, null, null);
                if (!ar.AsyncWaitHandle.WaitOne(3000)) return false;
                client.EndConnect(ar);
                using (var ssl = new SslStream(client.GetStream(), false, (_, __, ___, ____) => true))
                {
                    ssl.AuthenticateAsClient(host);
                    return true;
                }
            }
        }

        // ── Display ───────────────────────────────────────────────────────────

        private void UpdateText()
        {
            if (label == null) return;

            var sb = new System.Text.StringBuilder();
            sb.Append("Photon (IP|HTTP|WSS): ");

            for (int i = 0; i < Servers.Length; i++)
            {
                sb.Append($"{Servers[i].key} ");
                for (int j = 0; j < 3; j++)
                {
                    bool? st = statuses[i, j];
                    string color = st == null ? "#ffff00" : (st.Value ? "#00ff00" : "#ff4444");
                    string icon = st == null ? "?" : (st.Value ? "✓" : "✗");
                    sb.Append($"<color={color}>{icon}</color>");
                }
                if (i < Servers.Length - 1) sb.Append(", ");
            }

            label.text = sb.ToString();
        }
    }
}
