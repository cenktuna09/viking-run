using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gate : MonoBehaviour
{
    public Country country;

    [Header("Props")]
    public GameObject collectables;
    public GameObject landMarks;

    [Header("Character")]
    public GameObject model;
    /*
     
    public void ChangeTheme()
    {
        LevelManager.instance.GetActiveLevel().CloseAllProps();

        LevelManager.instance.GetActiveLevel().player.ChangeModel(model);

        if(collectables!=null)
            collectables.SetActive(true);

        if(landMarks!=null)
            landMarks.SetActive(true);

        if (country == Country.england)
            LevelManager.instance.GetActiveLevel().player.rainParticle.SetActive(true);
        else
            LevelManager.instance.GetActiveLevel().player.rainParticle.SetActive(false);

        IncrementCPercentage(5f);
    }
    */

    public void SetCountryPercentage(float value)
    {
        PlayerPrefs.SetFloat(country.ToString(), value);
    }

    public void IncrementCPercentage(float newPer)
    {
        PlayerPrefs.SetFloat(country.ToString(), GetCountryPercentage() + newPer);
    }

    public float GetCountryPercentage()
    {
        float percentage = PlayerPrefs.GetFloat(country.ToString(), 0);

        return percentage;
    }
}
public enum Country
{
    usa,
    italy,
    england,

}
