using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillBehaviour : MiningBuilding
{
   [SerializeField] private int currentHp;
   [SerializeField] private int maxBuildingHP;
   [SerializeField] private int level;
   [SerializeField] private string name;
    
   public override void Awake()
   {
      base.Awake();
      productionResType = ResType.Food;

      InitStatFields();
   }

   private void InitStatFields()
   {
      CurrentBuildingHP = currentHp;
      MaxBuildingHP = maxBuildingHP;
      Level = level;
      Name = name;
   }

   private List<Person> _class = new List<Person>()
   {
      new Person(15, "Anton", "Gandon"),
      new Person(18, "Vasa", "Pupka"),
      new Person(10, "Foma", "Tura")
   };
}

public class Person
{
   private int age;
   private string Name;
   private string SurName;
   
   public Person(int age, string name, string surName)
   {
      this.age = age;
      Name = name;
      SurName = surName;
   }
}
