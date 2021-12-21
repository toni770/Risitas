using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField] private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSduVgy-CQVCGLg37y7HM8_NhNThcRZ8QIHfNG7eI6FGp-MfHg/formResponse";
    public void Send(UserData userInfo)
    {
        StartCoroutine(Post(userInfo._nombre, userInfo._apellido, userInfo._correo));
    }

    IEnumerator Post(string nombre, string apellido, string correo)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.498932353", nombre);
        form.AddField("entry.568946969", apellido);
        form.AddField("entry.886727542", correo);
        byte[] rawData = form.data;
        
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;
    }
}
