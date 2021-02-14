using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class SemanticSearchEngine
{
    // URL for accessing elastic search.
    static string baseURL = "http://crest-cache-01.cs.fiu.edu:81/proceedings/proceeding/_search?q=";

    /// <summary>
    /// Get the search results from the semantic search engine.
    /// If the output is NULL, It was not successful.
    /// </summary>
    public static async Task<SearchResultData> GetSearchResultsAsync(string searchTerms, SearchType type)
    {
        // Create query based on search type.
        string queryFormat;
        switch (type)
        {
            case SearchType.abstractText:
                queryFormat = "abstract:{0} ";
                break;
            case SearchType.fullText:
                queryFormat = "full_text:{0} ";
                break;
            default:
                throw new InvalidOperationException("Invalid Search type!");
        }

        // Create URL using search terms and query type, and send API request, validate string as well.
        string[] terms = Regex.Split(searchTerms, @"[^A-Za-z0-9]+");
        string url = baseURL;

        // add multiple of the same query arg for each separate term to act as a match query type in json.
        foreach (string t in terms)
        {
            if (!t.Equals("")) url += string.Format(queryFormat, t);
        }

        UnityWebRequest www = UnityWebRequest.Get(url);
        Debug.Log(www.url);

        await www.SendWebRequest();

        // make sure no errors occur.
        if (www.isNetworkError || www.isHttpError) throw new InvalidOperationException(www.error);
        else
        {
            var searchResults = JsonUtility.FromJson<SearchResultData>(www.downloadHandler.text);

            Debug.Log(searchResults.hits.total);
            Debug.Log(searchResults.hits.max_score);

            return searchResults;
        }
    }
}

/// <summary>
/// Search types for queries.
/// </summary>
public enum SearchType
{
    abstractText = 0,
    fullText = 1
}

/// <summary>
/// Different classes for reading the JSON data from the semantic search engine.
/// </summary>
[Serializable]
public class SearchResultData
{
    public int took;
    public bool timed_out;
    public SearchHits hits;
}

[Serializable]
public class SearchHits
{
    public int total;
    public float max_score;
    public SearchHit[] hits;
}

[Serializable]
public class SearchHit
{
    public string _index;
    public string _type;
    public string _id;
    public float _score;
    public AcademicPaper _source;
}

[Serializable]
public class AcademicPaper
{
    public string title;
    public author[] authors;
    public int year;
    public string[] keywords;
    public string @abstract;
    public string category;
    public string full_text;

    public string printAuthors()
    {
        string auth = "";
        foreach (var a in authors)
        {
            auth += string.Format("{0}, ", a.name);
        }
        // remove last 2 chars ", "
        return auth.Substring(0, auth.Length - 2);
    }
}

[Serializable]
public class author
{
    public string name;
}
