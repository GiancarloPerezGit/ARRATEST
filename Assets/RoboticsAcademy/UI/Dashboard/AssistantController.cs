using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssistantController : MonoBehaviour
{
    [SerializeField] TMP_InputField inputText;
    [SerializeField] TextMeshProUGUI searchText;
    [SerializeField] Button submitButton;

    private void Awake()
    {
        submitButton.onClick.AddListener(delegate { AssistantSearch(); });
    }
    public async void AssistantSearch()
    {
        searchText.text = await DashboardSearchAsync(inputText.text, SearchType.abstractText);
    }

    /// <summary>
    /// Dashboard search functionality, returs a string with the title/authors to each relevant paper.
    /// </summary>
    public async Task<string> DashboardSearchAsync(string searchTerms, SearchType type)
    {
        try
        {
            var results = await SemanticSearchEngine.GetSearchResultsAsync(searchTerms, type);

            /*if (hits.Length != 0) return GetMostRelevantSentence(hits[0]._source.full_text, searchTerms);
            else throw new NullReferenceException("no results!");*/
            if (results.hits.hits != null && results.hits.hits.Length != 0)
            {
                var hits = results.hits.hits;
                int i = 0;
                string links = string.Format("<b>Showing first {0} results from {1} found.</b>\n\n", hits.Length, results.hits.total);
                foreach (var h in hits)
                {
                    links += string.Format("[{0}]   <i>{1}</i>, \"{2}\"\n\n", i, h._source.printAuthors(), h._source.title);
                    ++i;
                }
                return links;
            }
            else return "No Results. Try a different search.";
        }
        catch(InvalidOperationException e)
        {
            return "Connection to search was unsuccesful. Check connection and try again.";
        }
    }


    /// <summary>
    /// Function that implements algorithm to get the most relevant sentence in a block of text using search terms.
    /// </summary>
    public string GetMostRelevantSentence(string text, string searchTerms)
    {
        var sentenceScorer = new Dictionary<string, int>();
        if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(searchTerms))
        {
            string[] sentences = SplitAndKeep(text, new char[] { '.', '?', '!' });
            string[] terms = searchTerms.Split(new char[] { '.', '?', '!', ' ' });

            foreach (var sentence in sentences)
            {
                if (!sentenceScorer.ContainsKey(sentence) && !sentence.Equals("")) sentenceScorer.Add(sentence, 0);
            }

            foreach (var sentence in sentences)
            {
                foreach (var term in terms)
                {
                    if (sentence.Contains(term)) sentenceScorer[sentence]++;
                }
            }
            return sentenceScorer.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        }
        else return "No Results. Try a different search.";
    }

    /// <summary>
    /// Basic function for keeping characters that were used for splitting.
    /// </summary>
    public string[] SplitAndKeep(string s, char[] delims)
    {
        var strings = new List<string>();
        int start = 0, index;

        while ((index = s.IndexOfAny(delims, start)) != -1)
        {
            if (index - start > 0)
                strings.Add(s.Substring(start, index - start + 1));

            start = index + 1;
        }
        return strings.ToArray();
    }
}
