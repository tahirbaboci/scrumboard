using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestFunctions {


    Login l = new Login();
    public bool girisYapiliyor(string username, string password)
    {
        l.callLoginUser();
        if (l.loginSuccessfully)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AdetSayi(string adet)
    {
        if (IsDigit(adet))
        {
            return true;
        }
        else
        {
            return false;
        }
        throw new InvalidOperationException();
    }
    public bool AdetBos(string adet)
    {
        if (adet.Equals(""))
        {
            return true;
        }
        else
        {
            return false;
        }
        throw new InvalidOperationException();
    }
    public bool adetSifirIleMiBasliyor(string adet)
    {
        if (adet.StartsWith(0.ToString()))
        {
            return true;
        }
        else
        {
            return false;
        }
        throw new InvalidOperationException();
    }

    //public bool 


    public bool IsDigit(string value)
    {
        int sayac = 0;
        foreach (char i in value)
        {
            if (char.IsDigit(i))
            {
                sayac++;
            }
        }
        if (sayac == value.Length)
        {
            return true;
        }
        return false;
    }
    public bool IsLetter(string value)
    {
        int sayac = 0;
        foreach (char i in value)
        {
            if (char.IsLetter(i))
            {
                sayac++;
            }
        }
        if (sayac == value.Length)
        {
            return true;
        }
        return false;
    }
    public bool IsPunctuation(string value)
    {
        int sayac = 0;
        foreach (char i in value)
        {
            if (char.IsPunctuation(i))
            {
                sayac++;
            }
        }
        if (sayac == value.Length)
        {
            return true;
        }
        return false;
    }
    public bool IsWhitespace(string value)
    {
        int sayac = 0;
        foreach (char i in value)
        {
            if (char.IsWhiteSpace(i))
            {
                sayac++;
            }
        }
        if (sayac == value.Length)
        {
            return true;
        }
        return false;
    }

    public bool SQLInjection(string value)
    {
        int sayac = 0;
        foreach (char i in value)
        {
            if (i.Equals("'"))
            {
                sayac++;
            }
        }
        if (sayac >= 1)
        {
            return true;
        }
        return false;
    }
}
