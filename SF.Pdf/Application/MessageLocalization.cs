using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Pdf.Application;
public class MessageLocalization
{
    private static Dictionary<string, string> defaultLanguage = new();
    private static Dictionary<string, string> currentLanguage;
    private const string BASE_PATH = "iTextSharp.text.error_messages.";

    private MessageLocalization()
    {
    }

    static MessageLocalization()
    {
        try
        {
            defaultLanguage = GetLanguageMessages("en", null);
        }
        catch
        {
            // do nothing
        }
        if (defaultLanguage == null)
            defaultLanguage = new Dictionary<string, string>();
    }

    /**
    * Get a message without parameters.
    * @param key the key to the message
    * @return the message
    */
    public static string GetMessage(string key)
    {
        return GetMessage(key, true);
    }


    public static string GetMessage(string key, bool useDefaultLanguageIfMessageNotFound)
    {
        var cl = currentLanguage;
        string val;
        if (cl != null)
        {
            cl.TryGetValue(key, out val);
            if (val != null)
                return val;
        }

        if (useDefaultLanguageIfMessageNotFound)
        {
            cl = defaultLanguage;
            cl.TryGetValue(key, out val);
            if (val != null)
                return val;
        }

        return "No message found for " + key;
    }

    /**
    * Get a message with parameters. The parameters will replace the strings
    * "{1}", "{2}", ..., "{n}" found in the message.
    * @param key the key to the message
    * @param p the variable parameter
    * @return the message
    */
    public static string GetComposedMessage(string key, params object[] p)
    {
        var msg = GetMessage(key);
        for (var k = 0; k < p.Length; ++k)
        {
            msg = msg.Replace("{" + (k + 1) + "}", p[k].ToString());
        }
        return msg;
    }

    /**
    * Sets the language to be used globally for the error messages. The language
    * is a two letter lowercase country designation like "en" or "pt". The country
    * is an optional two letter uppercase code like "US" or "PT".
    * @param language the language
    * @param country the country
    * @return true if the language was found, false otherwise
    * @throws IOException on error
    */
    public static bool SetLanguage(string language, string country)
    {
        var lang = GetLanguageMessages(language, country);
        if (lang == null)
            return false;
        currentLanguage = lang;
        return true;
    }

    /**
    * Sets the error messages directly from a Reader.
    * @param r the Reader
    * @throws IOException on error
    */
    public static void SetMessages(TextReader r)
    {
        currentLanguage = ReadLanguageStream(r);
    }

    private static Dictionary<string, string> GetLanguageMessages(string language, string country)
    {
        if (language == null)
            throw new ArgumentException("The language cannot be null.");
        Stream isp = null;
        try
        {
            string file;
            if (country != null)
                file = language + "_" + country + ".lng";
            else
                file = language + ".lng";
            isp = StreamUtil.GetResourceStream(BASE_PATH + file);
            if (isp != null)
                return ReadLanguageStream(isp);
            if (country == null)
                return null;
            file = language + ".lng";
            isp = StreamUtil.GetResourceStream(BASE_PATH + file);
            if (isp != null)
                return ReadLanguageStream(isp);
            else
                return null;
        }
        finally
        {
            try
            {
                isp.Close();
            }
            catch
            {
            }
            // do nothing
        }
    }

    private static Dictionary<string, string> ReadLanguageStream(Stream isp)
    {
        return ReadLanguageStream(new StreamReader(isp, Encoding.UTF8));
    }

    private static Dictionary<string, string> ReadLanguageStream(TextReader br)
    {
        var lang = new Dictionary<string, string>();
        string line;
        while ((line = br.ReadLine()) != null)
        {
            var idxeq = line.IndexOf('=');
            if (idxeq < 0)
                continue;
            var key = line.Substring(0, idxeq).Trim();
            if (key.StartsWith("#"))
                continue;
            lang[key] = line.Substring(idxeq + 1);
        }
        return lang;
    }
}
