﻿using ArenaPro.Domain.Utils;
using ArenaPro.Domain.Validations;
using System.Xml.Linq;

namespace ArenaPro.Domain.Entities;
public class Player : Entity
{
    protected Player()
    {
        
    }

    public Player(string nick,int? teamId = null, string? name = null, int? age = null, string? genre = null, string? email = null)
    {
        var cleanNick = nick.Replace(" ", "");
        DomainException.When(cleanNick.Length < 4, "Nick must have at least 4 characters");
        DomainException.When(age < 18 && age != -1, "Age could not be less than 18 years");

        Nick = cleanNick;
        Name = name;
        Age = age;
        TeamId = teamId;
        Genre = genre;
        Email = email;
    }

    public void ChangeNick(string nick)
    {
        if (string.IsNullOrEmpty(nick)) DomainException.When(true, "String was empty");
        DomainException.When(nick.Length < 4, "Nick must have at least 4 characters");
        Nick = nick;
    }

    public void ChangeAge(int age)
    {
        DomainException.When(age < 18, "Age could not be less than 18 years");
        Age = age;
    }

    public void ChangeTeam(Team team)
    {
        DomainException.When(team == null, "Team could not be NULL");
        Team = team!;
        TeamId = team!.Id;
    }

    public void ChangeEmail(string email)
    {
        DomainException.When(!EmailUtils.IsValidEmail(email), "Email is invalid");
        Email = email;
    }

    public string Nick { get; private set; }
    public string? Name { get; set; }
    public int? Age { get; private set; }
    public string? Genre { get; set; }
    public string? Email { get; private set; }
    public int? TeamId { get; private set; }
    public virtual Team Team { get; private set; }
}
