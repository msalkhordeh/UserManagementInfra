using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.String;

namespace UM.Utility
{
    public static class ApiExtension
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content) where T : class
        {
            var json = await content.ReadAsStringAsync();
            if (json.IsValidJson())
            {
                var value = JsonConvert.DeserializeObject<T>(json);
                return value;
            }

            return json as T;
        }

        public static T ReadAsJson<T>(this HttpContent content) where T : class
        {
            var json = content.ReadAsStringAsync().Result;
            if (json.IsValidJson())
            {
                var value = JsonConvert.DeserializeObject<T>(json);
                return value;
            }

            return json as T;
        }

        public static async Task<Stream> GetPdfAsync(string baseAddress, string urlPathAndQuery,
            IDictionary<string, string> headers = default,
            CancellationToken cancellationToken = default)
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true
            };
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                if (headers != default)
                {
                    foreach (var item in headers)
                    {
                        if (client.DefaultRequestHeaders.Contains(item.Key))
                        {
                            client.DefaultRequestHeaders.Remove(item.Key);
                        }

                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                try
                {
                    var response = await client.GetAsync(urlPathAndQuery, cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var obj = await response.Content.ReadAsStreamAsync(cancellationToken);
                        return obj;
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            }
            return default;
        }

        public static async Task<T> GetAsync<T>(string baseAddress, string urlPathAndQuery,
            string token = default, IDictionary<string, string> headers = default,
             CancellationToken cancellationToken = default) where T : class
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true
            };
            using var client = new HttpClient(clientHandler) { BaseAddress = new Uri(baseAddress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != default)
            {
                client.DefaultRequestHeaders.Add("Security-Token", token);
            }
            if (headers != default)
            {
                foreach (var item in headers)
                {
                    if (client.DefaultRequestHeaders.Contains(item.Key))
                    {
                        client.DefaultRequestHeaders.Remove(item.Key);
                    }

                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            try
            {
                var response = await client.GetAsync(urlPathAndQuery, cancellationToken);
                var obj = await response.Content.ReadAsJsonAsync<T>();
                return obj;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static async Task<T> PostAsync<T, TParam>(string baseAddress, string urlPath,
            TParam param, string token = default, IDictionary<string, string> headers = default,
            CancellationToken cancellationToken = default) where T : class
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true
            };
            using var client = new HttpClient(clientHandler) { BaseAddress = new Uri(baseAddress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != default)
            {
                client.DefaultRequestHeaders.Add("Security-Token", token);
            }
            if (headers != default)
            {
                foreach (var item in headers)
                {
                    if (client.DefaultRequestHeaders.Contains(item.Key))
                    {
                        client.DefaultRequestHeaders.Remove(item.Key);
                    }

                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            try
            {
                var json = JsonConvert.SerializeObject(param);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(urlPath, data, cancellationToken);
                var obj = await response.Content.ReadAsJsonAsync<T>();
                return obj;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static async Task<T> PutAsync<T, TParam>(string baseAddress, string urlPath,
            TParam param, string token = default, IDictionary<string, string> headers = default,
            CancellationToken cancellationToken = default) where T : class
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true
            };
            using var client = new HttpClient(clientHandler) { BaseAddress = new Uri(baseAddress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != default)
            {
                client.DefaultRequestHeaders.Add("Security-Token", token);
            }
            if (headers != default)
            {
                foreach (var item in headers)
                {
                    if (client.DefaultRequestHeaders.Contains(item.Key))
                    {
                        client.DefaultRequestHeaders.Remove(item.Key);
                    }

                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            try
            {
                var json = JsonConvert.SerializeObject(param);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(urlPath, data, cancellationToken);
                var obj = await response.Content.ReadAsJsonAsync<T>();
                return obj;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static async Task<T> PatchAsync<T, TParam>(string baseAddress, string urlPath,
            TParam param, string token = default, IDictionary<string, string> headers = default,
            CancellationToken cancellationToken = default) where T : class
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true
            };
            using var client = new HttpClient(clientHandler) { BaseAddress = new Uri(baseAddress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != default)
            {
                client.DefaultRequestHeaders.Add("Security-Token", token);
            }
            if (headers != default)
            {
                foreach (var item in headers)
                {
                    if (client.DefaultRequestHeaders.Contains(item.Key))
                    {
                        client.DefaultRequestHeaders.Remove(item.Key);
                    }

                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            try
            {
                var json = JsonConvert.SerializeObject(param);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PatchAsync(urlPath, data, cancellationToken);
                var obj = await response.Content.ReadAsJsonAsync<T>();
                return obj;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static async Task<T> PostFormUrlEncodedAsync<T>(string baseAddress, string urlPath,
            IDictionary<string, string> parameters, IDictionary<string, string> headers = default,
           CancellationToken cancellationToken = default) where T : class
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => true
            };
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                if (headers != default)
                {
                    foreach (var item in headers)
                    {
                        if (client.DefaultRequestHeaders.Contains(item.Key))
                        {
                            client.DefaultRequestHeaders.Remove(item.Key);
                        }

                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                try
                {
                    var data = new FormUrlEncodedContent(parameters);
                    var response = await client.PostAsync(urlPath, data, cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var obj = await response.Content.ReadAsJsonAsync<T>();
                        return obj;
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            }
            return default;
        }

        public static async Task<Stream> PostReturnPdfAsync<TParam>(string baseAddress, string urlPath, TParam param, string token = null,
         IDictionary<string, string> headers = null, NamingStrategy namingStrategy = null, CancellationToken cancellationToken = default)
        {
            using var client = new HttpClient(new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            })
            {
                BaseAddress = new Uri(baseAddress)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != null)
                client.DefaultRequestHeaders.Add("Security-Token", token);
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in (IEnumerable<KeyValuePair<string, string>>)headers)
                {
                    if (client.DefaultRequestHeaders.Contains(header.Key))
                        client.DefaultRequestHeaders.Remove(header.Key);
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            try
            {
                string content;
                if (namingStrategy != null)
                    content = JsonConvert.SerializeObject(param, new JsonSerializerSettings()
                    {
                        ContractResolver = new DefaultContractResolver()
                        {
                            NamingStrategy = namingStrategy
                        },
                        Formatting = (Formatting)1,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                else
                    content = JsonConvert.SerializeObject(param);
                Encoding utF8 = Encoding.UTF8;
                return await (await client.PostAsync(urlPath, new StringContent(content, utF8, "application/json"), cancellationToken)).Content.ReadAsStreamAsync(cancellationToken);
            }
            catch
            {
                return null;
            }
        }

        public static async Task<T> PostFormDataAsync<T, TParam>(string baseAddress, string urlPath,
            TParam param, CancellationToken cancellationToken = default) where T : class
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true,
                MaxRequestContentBufferSize = int.MaxValue,
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseAddress);
                client.Timeout = TimeSpan.FromSeconds(300);
                client.DefaultRequestHeaders.Add("User-Agent", "Mogli Server EWallet/.Net Core EPay 1.0.0");
                var content = new MultipartFormDataContent();
                foreach (var prop in param.GetType().GetProperties())
                {
                    var value = prop.GetValue(param);
                    switch (value)
                    {
                        case null:
                            continue;
                        case IFormFile formFile:
                            {
                                var file = formFile;
                                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                                break;
                            }
                        case List<IFormFile> formFiles:
                            {
                                foreach (var formFile in formFiles)
                                {
                                    var file = formFile;
                                    content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                                }
                                break;
                            }
                        case List<string> values:
                            {
                                foreach (var item in values)
                                {
                                    content.Add(new StringContent(item), prop.Name);
                                }
                                break;
                            }
                        default:
                            content.Add(new StringContent(value.ToString()), prop.Name);
                            break;
                    }
                }

                try
                {
                    var response = await client.PostAsync(urlPath, content, cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var obj = await response.Content.ReadAsJsonAsync<T>();
                        return obj;
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            }
            return default;
        }

        public static async Task<string> PostFormFileAsync(string baseAddress, string urlPath,
            IFormFile file, string token = default, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length <= 0)
            {
                return Empty;
            }

            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true
            };
            using var client = new HttpClient(clientHandler);
            try
            {
                client.BaseAddress = new Uri(baseAddress);
                if (token != default)
                {
                    client.DefaultRequestHeaders.Add("Security-Token", token);
                }
                byte[] data;
                using (var br = new BinaryReader(file.OpenReadStream()))
                {
                    data = br.ReadBytes((int)file.OpenReadStream().Length);
                }

                var bytes = new ByteArrayContent(data);
                var multiContent = new MultipartFormDataContent
                    {
                        {bytes, "file", file.FileName}
                    };
                var response = await client.PostAsync(urlPath, multiContent, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    return Empty;
                }

                var obj = await response.Content.ReadAsStringAsync(cancellationToken);
                return obj;
            }
            catch (Exception)
            {
                return Empty;
            }
        }


        public static T Get<T>(string baseAddress, string urlPathAndQuery,
        string token = default, IDictionary<string, string> headers = default) where T : class
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true
            };
            using var client = new HttpClient(clientHandler) { BaseAddress = new Uri(baseAddress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != default)
            {
                client.DefaultRequestHeaders.Add("Security-Token", token);
            }
            if (headers != default)
            {
                foreach (var item in headers)
                {
                    if (client.DefaultRequestHeaders.Contains(item.Key))
                    {
                        client.DefaultRequestHeaders.Remove(item.Key);
                    }

                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            try
            {
                var response =  client.GetAsync(urlPathAndQuery).Result;
                var obj = response.Content.ReadAsJson<T>();
                return obj;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T Post<T, TParam>(string baseAddress, string urlPath,
            TParam param, string token = default, IDictionary<string, string> headers = default) 
            where T : class
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true
            };
            using var client = new HttpClient(clientHandler) { BaseAddress = new Uri(baseAddress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != default)
            {
                client.DefaultRequestHeaders.Add("Security-Token", token);
            }
            if (headers != default)
            {
                foreach (var item in headers)
                {
                    if (client.DefaultRequestHeaders.Contains(item.Key))
                    {
                        client.DefaultRequestHeaders.Remove(item.Key);
                    }

                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            try
            {
                var json = JsonConvert.SerializeObject(param);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response =  client.PostAsync(urlPath, data).Result;
                var obj =  response.Content.ReadAsJson<T>();
                return obj;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T Put<T, TParam>(string baseAddress, string urlPath,
            TParam param, string token = default, IDictionary<string, string> headers = default) where T : class
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true
            };
            using var client = new HttpClient(clientHandler) { BaseAddress = new Uri(baseAddress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != default)
            {
                client.DefaultRequestHeaders.Add("Security-Token", token);
            }
            if (headers != default)
            {
                foreach (var item in headers)
                {
                    if (client.DefaultRequestHeaders.Contains(item.Key))
                    {
                        client.DefaultRequestHeaders.Remove(item.Key);
                    }

                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            try
            {
                var json = JsonConvert.SerializeObject(param);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response =  client.PutAsync(urlPath, data).Result;
                var obj =  response.Content.ReadAsJson<T>();
                return obj;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T Patch<T, TParam>(string baseAddress, string urlPath,
            TParam param, string token = default, IDictionary<string, string> headers = default)
            where T : class
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true
            };
            using var client = new HttpClient(clientHandler) { BaseAddress = new Uri(baseAddress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != default)
            {
                client.DefaultRequestHeaders.Add("Security-Token", token);
            }
            if (headers != default)
            {
                foreach (var item in headers)
                {
                    if (client.DefaultRequestHeaders.Contains(item.Key))
                    {
                        client.DefaultRequestHeaders.Remove(item.Key);
                    }

                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            try
            {
                var json = JsonConvert.SerializeObject(param);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PatchAsync(urlPath, data).Result;
                var obj = response.Content.ReadAsJson<T>();
                return obj;
            }
            catch (Exception)
            {
                return default;
            }
        }

        private static bool IsValidJson(this string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return false;
            }

            var value = stringValue.Trim();
            if (value.StartsWith("{", StringComparison.Ordinal) &&
                value.EndsWith("}", StringComparison.Ordinal) || //For object
                value.StartsWith("[", StringComparison.Ordinal) &&
                value.EndsWith("]", StringComparison.Ordinal)) //For array
            {
                try
                {
                    var _ = JToken.Parse(value);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }
    }
}