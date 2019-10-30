using System.Collections;
using UnityEngine;
using System;
using HtmlAgilityPack;

public class DownloadFromSentera : MonoBehaviour {
    // Use this for initialization
    public String photoLink;
    Renderer myRenderer;
    void Start () {
        myRenderer = GetComponent<Renderer>();
    }

    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("space")) {
            var html = @"http://192.168.143.141/last_img?lnk=li0&camera=1";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//p/a");

            if (node != null) {
                //Debug.Log("Node Name: " + node.Name + "\n" + node.OuterHtml);
                //Debug.Log(node);
                photoLink = "http://192.168.143.141" + node.Attributes["href"].Value;
                Debug.Log(photoLink);
                StartCoroutine(Download(photoLink));

            }
            else
            {
                Debug.Log("Selection is null");
            }
        }
    }
    

    
    IEnumerator Download(String url)
    {
        //Debug.Log("Entering function");
        using (WWW www = new WWW(url))
        {
            Debug.Log("Download started");
            // Wait for download to complete
            yield return www;

            Debug.Log("Download completed");
            myRenderer.material.mainTexture = www.texture;
        }
    }

}
