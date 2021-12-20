using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using TMPro;

public class EmailSender : MonoBehaviour
{
    private bool triggerResultEmail = false;
    private bool resultEmailSuccess;

    [SerializeField] private string SMTPCLient;
    [SerializeField] private int SMTPPort;
    [SerializeField] private string UserName;
    [SerializeField] private string UserPasswd;
    [SerializeField] private string To;

    EmailFactory emailFactory;

    private void Awake()
    {
        emailFactory = GetComponent<EmailFactory>();
    }
    public void SendMail(string subject, string body)
    {
        /*  SimpleEmailSender.emailSettings.STMPClient = SMTPCLient;
        SimpleEmailSender.emailSettings.SMTPPort = SMTPPort;
        SimpleEmailSender.emailSettings.UserName = UserName;
        SimpleEmailSender.emailSettings.UserPass = UserPasswd;

        SimpleEmailSender.Send(To, subject, body, "", SendCompleteCallCallBack);*/

        emailFactory.SendEmail(SMTPCLient, SMTPPort, UserName, UserPasswd, To, subject, body);


    }
    private void SendCompleteCallCallBack(object sender, AsyncCompletedEventArgs e) 
    {
        if(e.Cancelled || e.Error!= null)
        {
            print("Email not sent: " + e.Error.ToString());
            resultEmailSuccess = false;
            triggerResultEmail = true;
        }
        else
        {
            print("Email  succesfully sent.");

            resultEmailSuccess = true;
            triggerResultEmail = true;
        }
    }
}
