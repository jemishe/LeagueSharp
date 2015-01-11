﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace NiceGuySharp
{
    public class Program
    {
        public static Menu Menu;
        public static Random Rand = new Random(Environment.TickCount);
        public static Obj_AI_Hero MyHero = ObjectManager.Player;
        public static List<Obj_AI_Hero> AllHeroes;
        public static List<Obj_AI_Hero> AllyHeroes;
        public static List<Obj_AI_Hero> EnemyHeroes;
        public static int Deaths,
            Kills,
            Doubles,
            Triples,
            Quadras,
            Pentas,
            AllyKills,
            AllyDeaths,
            AllyDoubles,
            AllyQuadras,
            AllyPentas,
            EnemyKills,
            EnemyDeaths,
            EnemyDoubles,
            EnemyTriples,
            EnemyQuadras,
            EnemyPentas = 0;

        public static int LastSentMessage;
        public static int MinTimeBeforeNewMessage = Rand.Next(5000, 60000);
        public static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        public static void Game_OnGameLoad(EventArgs args)
        {

            Menu = new Menu("Nice Guy Sharp", "niceguy", true);
            Menu.AddItem(new MenuItem("enabled", "Enable").SetValue(true));
            
            Menu.AddToMainMenu();
            FileHandler.DoChecks();
            AllHeroes = ObjectManager.Get<Obj_AI_Hero>().ToList();
            AllyHeroes = AllHeroes.FindAll(hero => hero.IsAlly).ToList();
            EnemyHeroes = AllHeroes.FindAll(hero => !hero.IsAlly).ToList();
            Game.PrintChat("NICE-GUY SHARP SUCCESFULLY LOADED.");
            
            string[] onGameStart = File.ReadAllLines(FileHandler.OnGameStartTxt);
            int randMessage = Rand.Next(onGameStart.Count());
            if (onGameStart != null && onGameStart.Length > 0)
            {
                Game.Say(onGameStart[randMessage]);
            }

            Game.OnGameUpdate += Game_OnGameUpdate;
        }
        public static void Game_OnGameUpdate(EventArgs args)
        {
            if (Menu.Item("enabled").GetValue<bool>())
            {
                if (Environment.TickCount - LastSentMessage > MinTimeBeforeNewMessage)
                {

                    if (RealAllyPentas() > AllyPentas)
                    {
                        string[] onAllyPenta = File.ReadAllLines(FileHandler.OnAllyPentaTxt);
                        int randMessage = Rand.Next(onAllyPenta.Count());
                        if (onAllyPenta != null && onAllyPenta.Length > 0)
                        {
                            Game.Say(onAllyPenta[randMessage]);
                        }
                        AllyPentas = RealAllyPentas();
                    }
                    if (RealEnemyPentas() > EnemyPentas)
                    {
                        string[] onEnemyPenta = File.ReadAllLines(FileHandler.OnEnemyPentaTxt);
                        int randMessage = Rand.Next(onEnemyPenta.Count());
                        if (onEnemyPenta != null && onEnemyPenta.Length > 0)
                        {
                            Game.Say(onEnemyPenta[randMessage]);
                        }
                        EnemyPentas = RealEnemyPentas();
                    }

                    if (MyHero.Deaths > Deaths)
                    {
                        string[] onDeath = File.ReadAllLines(FileHandler.OnDeathTxt);
                        int randMessage = Rand.Next(onDeath.Count());
                        if (onDeath != null && onDeath.Length > 0)
                        {
                            Game.Say(onDeath[randMessage]);
                        }
                        Deaths = MyHero.Deaths;
                    }
                    if (MyHero.DoubleKills > Doubles)
                    {
                        string[] onDouble = File.ReadAllLines(FileHandler.OnDoubleTxt);
                        int randMessage = Rand.Next(onDouble.Count());
                        if (onDouble != null && onDouble.Length > 0)
                        {
                            Game.Say(onDouble[randMessage]);
                        }
                        Doubles = MyHero.DoubleKills;
                    }
                    if (MyHero.TripleKills > Triples)
                    {
                        string[] onTriple = File.ReadAllLines(FileHandler.OnTripleTxt);
                        int randMessage = Rand.Next(onTriple.Count());
                        if (onTriple != null && onTriple.Length > 0)
                        {
                            Game.Say(onTriple[randMessage]);
                        }
                        Triples = MyHero.TripleKills;
                    }
                    if (MyHero.QuadraKills > Quadras)
                    {
                        string[] onQuadra = File.ReadAllLines(FileHandler.OnQuadraTxt);
                        int randMessage = Rand.Next(onQuadra.Count());
                        if (onQuadra != null && onQuadra.Length > 0)
                        {
                            Game.Say(onQuadra[randMessage]);
                        }
                        Quadras = MyHero.QuadraKills;
                    }
                    if (MyHero.PentaKills > Pentas)
                    {
                        string[] onPenta = File.ReadAllLines(FileHandler.OnPentaTxt);
                        int randMessage = Rand.Next(onPenta.Count());
                        if (onPenta != null && onPenta.Length > 0)
                        {
                            Game.Say(onPenta[randMessage]);
                        }
                        Pentas = MyHero.PentaKills;
                    }
                    if (MyHero.ChampionsKilled > Kills)
                    {
                        string[] onKill = File.ReadAllLines(FileHandler.OnKillTxt);
                        int randMessage = Rand.Next(onKill.Count());
                        if (onKill != null && onKill.Length > 0)
                        {
                            Game.Say(onKill[randMessage]);
                        }
                        Kills = MyHero.ChampionsKilled;
                    }
                }
            }

        }

        public static int RealAllyPentas()
        {
            int pentakills = 0;
            foreach (var hero in AllyHeroes)
            {
                pentakills += hero.PentaKills;
            }
            return pentakills;
        }
        public static int RealEnemyPentas()
        {
            int pentakills = 0;
            foreach (var hero in EnemyHeroes)
            {
                pentakills += hero.PentaKills;
            }
            return pentakills;
        }
    }
}
