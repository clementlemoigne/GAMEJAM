//#define USEFACEBOOK
//#define USETWITTER
#define USEGITHUB

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SimpleShare : MonoBehaviour {

    //------------------------------------------------//
    //This Link is attached to this post
    public string Link = "";

#if USETWITTER
    /* TWITTER VARIABLES */

    //twitter Share Link
    string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";

    //Language
    string TWEET_LANGUAGE = "fr";

    //This is the text which you want to show
    string textToDisplay = "Voici mon score sur Road Band : ";

    /*END OF TWITTER VARIABLES*/
#endif

    //------------------------------------------------//

#if USEFACEBOOK
    /*FACEBOOK VARIABLES*/

    //App ID
    string AppId = "1138890502922250";

    

    //The URL of a picture attached to this post. The Size must be at least 200px by 200px
    //string Picture = "";

    //The Caption of the link appears beneath the link name
    //string Caption = "Regarder mon score sur Road Band: ";

    //string Descrition = "Enjoy Fun";
    /*END OF FACEBOOK VARIABLES*/

#endif

    //------------------------------------------------//

#if USEGITHUB
    public string LinkGitHub = "";
#endif

#if USETWITTER
    public void shareScoreOnTwitter()
    {
        Application.OpenURL(TWITTER_ADDRESS + "?text=" + WWW.EscapeURL(textToDisplay) + PlayerPrefs.GetInt("Score") + "&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE));
    }
#endif

#if USEFACEBOOK
    public void shareScoreOnFacebook()
    {
        Application.OpenURL("https://www.facebook.com/dialog/feed?" + "app_id=" + AppId + "&link=" + Link);
    }
#endif

#if USEGITHUB
    public void OpenGitHubPage()
    {
        Application.OpenURL(LinkGitHub);
    }
#endif

}
