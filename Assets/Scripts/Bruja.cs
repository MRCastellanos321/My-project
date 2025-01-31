
using System;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Tablero
{
    public class Bruja : characterInterface
    {
        private int attack = 2;
        private int turnsPassed = 0;
        private int attackCoolDown = 0;
        private int collectedShards = 0;
        private int skillCoolDown = 0;
        private int attackInmunity = 0;
        private int trapInmunity = 0;
        private int doorKeysCollected = 0;
        private int mazeVisibility = 0;
        private int diceEffect = 0;
        private int positionVisibility = 0;

        public static bool onTeleport = false;

        public int GetTrapInmunity()
        {
            return trapInmunity;
        }
        public void SetTrapInmunity(int value)
        {
            trapInmunity += value;
        }
        public int GetAttackInmunity()
        {
            return attackInmunity;
        }
        public void SetAttackInmunity(int value)
        {
            attackInmunity += value;
        }

        public int GetAttack()
        {
            return attack;
        }

        public int GetTurnsPassed()
        {
            return turnsPassed;
        }
        public void SetTurnsPassed(int number)
        {
            turnsPassed += number;
        }

        public int GetAttackCoolDown()
        {
            return attackCoolDown;
        }
        public void SetAttackCoolDown(int number)
        {
            attackCoolDown += number;
        }
        public int GetCollectedShards()
        {
            return collectedShards;
        }
        public void SetCollectedShards(int number)
        {
            collectedShards += number;
            if (collectedShards == 3)
            {
                Manager.Instancia.TurnInHuman();
            }
        }
        public int GetSkillCoolDown()
        {
            return skillCoolDown;
        }
        public void SetSkillCoolDown(int number)
        {
            skillCoolDown += number;
        }
        public void SetCollectedKeys(int value)
        {
            doorKeysCollected += value;
        }
        public int GetCollectedKeys()
        {
            return doorKeysCollected;
        }
        public int GetMazeVisibility()
        {
            return mazeVisibility;
        }
        public void SetMazeVisibility(int value)
        {
            mazeVisibility += value;
        }
        public int GetDiceEffect()
        {
            return diceEffect;
        }
        public void SetDiceEffect(int value)
        {
            diceEffect += value;
        }
        public int GetPositionVisibility()
        {
            return positionVisibility;
        }
        public void SetPositionVisibility(int value)
        {
            positionVisibility += value;
        }
        public void Skill()
        {
            System.Random random = new System.Random();
            int posRandomF;
            int posRandomC;
            bool playerOnPosition = false;
            bool teleported = false;
            while (!teleported)
            {
                posRandomF = random.Next(4, 48);
                posRandomC = random.Next(4, 48);

                int value = Laberinto.ElLaberinto.Read(posRandomF, posRandomC);

                if (value == 1 && (posRandomC > 28 || posRandomC < 22 || posRandomF > 28 || posRandomF < 22))
                {
                    for (int i = 0; i < Manager.FilasColumnas.Length; i++)
                    {
                        if (Manager.FilasColumnas[i][0] == posRandomF && Manager.FilasColumnas[i][1] == posRandomC && i != Manager.Instancia.currentPlayerIndex - 1)
                        {
                            playerOnPosition = true;
                            break;
                        }
                    }
                    if (!playerOnPosition)
                    {
                        //Manager.playersPosition[Manager.Instancia.currentPlayerIndex - 1].position = new Vector3(posRandomC * SpawnMaze.tileWidth, (Laberinto.ElLaberinto.GetSize() - posRandomF - 1) * SpawnMaze.tileWidth, 0);
                        Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0] = posRandomF;
                        Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1] = posRandomC;
                        //PlayerMovement.TeleportTarget(posRandomF, posRandomC);
                        //  PlayerMovement.targetPosition = new Vector3(posRandomC * SpawnMaze.tileWidth, (Laberinto.ElLaberinto.GetSize() - posRandomF - 1) * SpawnMaze.tileWidth, 0);
                        onTeleport = true;
                        teleported = true;
                        skillCoolDown += 5;
                    }
                }
            }
        }
    }
}
