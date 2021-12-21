using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string _nombre;
    public string _apellido;
    public string _correo;

    public UserData (string nombre = "", string apellido = "", string correo = "")
    {
        _nombre = nombre;
        _apellido = apellido;
        _correo = correo;
    }
}
