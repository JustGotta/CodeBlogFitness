﻿using CodeBlogFitness.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeBlogFitness.BL.Controller
{
    public class EatingController : ControllerBase
    {
        private const string FOODS_FILE_NAME = "foods.dat";
        private const string EATINGS_FILE_NAME = "eatings.dat";

        private readonly User user;
        public List<Food> Foods { get; }
        public Eating Eating { get; }


        public EatingController(User user)
        {
            this.user = user ?? throw new ArgumentNullException("Пользователь не может быть пустым.", nameof(user));
            Foods = GetAllFoods();
            Eating = GetEating();
        }

        public void Add(Food food, double weight)
        {
            var product = Foods.SingleOrDefault(f => f.Name == food.Name);
            if (product==null)
            {
                Foods.Add(food);
                Eating.Add(food, weight);
                Save();
            }
            else
            {
                Eating.Add(product,weight);
                Save();
            }
        }

        private Eating GetEating()
        {
            return base.Load<Eating>(EATINGS_FILE_NAME) ?? new Eating(user);
        }

        private List<Food> GetAllFoods()
        {
            return base.Load<List<Food>>(FOODS_FILE_NAME) ?? new List<Food>();
        }

        private void Save()
        {
            base.Save(FOODS_FILE_NAME, Foods);
            base.Save(EATINGS_FILE_NAME, Eating);
        }
    }
}
