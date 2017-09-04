using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShare : MonoBehaviour {

    /* TWITTER VARIABLES */

    //twitter Share Link
    string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";

    //Language
    string TWEET_LANGUAGE = "fr";

    //This is the text which you want to show
    string textToDisplay = "Voici mon score sur Road Band : ";

    /*END OF TWITTER VARIABLES*/

    /*FACEBOOK VARIABLES*/

    //App ID
    string AppId = "1138890502922250";

    //This Link is attached to this post
    
    public string Link = "https://google.com";

    public string LinkGitHub = "";
    //The URL of a picture attached to this post. The Size must be at least 200px by 200px
    string Picture = "https://s1-ssl.dmcdn.net/B6Qqu/200x200-bX4.jpg";

    //The Caption of the link appears beneath the link name
    string Caption = "Regarder mon score sur Road Band: ";

    string Descrition = "Enjoy Fun";
    /*END OF FACEBOOK VARIABLES*/

    public void shareScoreOnTwitter()
    {
        Application.OpenURL(TWITTER_ADDRESS + "?text=" + WWW.EscapeURL(textToDisplay) + PlayerPrefs.GetInt("Score") + "&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE));
    }

    public void shareScoreOnFacebook()
    {
        Application.OpenURL("https://www.facebook.com/dialog/feed?" + "app_id=" + AppId + "&link=" + Link);
    }

    public void OpenGitHubPage()
    {
        Application.OpenURL(LinkGitHub);
    }



}
