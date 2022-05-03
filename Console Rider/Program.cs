using System;

namespace Console_Rider
{
    internal static class Program
    {
        static void Main()
        {
            const int height = 30;
            const int width = 50;
            Random random = new Random();
            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            string gameOverInfo = null;
            int sceneId = 0;
            int sceneIndex = 0;


            while (true)
            {
                switch (sceneId)
                {
                    case 0:
                        bool startGame = false;
                        Console.Clear();
                        Console.SetCursorPosition(width / 2 - 2, 8);
                        Console.Write("骑士");
                        while (true)
                        {
                            Console.ForegroundColor = sceneIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.SetCursorPosition(width / 2 - 4, 12);
                            Console.Write("开始游戏");

                            Console.ForegroundColor = sceneIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.SetCursorPosition(width / 2 - 4, 14);
                            Console.Write("结束游戏");

                            char c = Console.ReadKey(true).KeyChar;
                            switch (c)
                            {
                                case 'w':
                                case 'W':
                                    sceneIndex = 0;
                                    break;

                                case 's':
                                case 'S':
                                    sceneIndex = 1;
                                    break;

                                case 'j':
                                case 'J':
                                    if (sceneIndex == 0)
                                    {
                                        sceneId = 1;
                                        startGame = true;
                                    }
                                    else
                                    {
                                        Environment.Exit(0);
                                    }

                                    break;
                            }

                            if (startGame) break;
                        }

                        break;

                    case 1:

                        #region 绘制墙

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        for (int i = 0; i < width; i += 2)
                        {
                            Console.SetCursorPosition(i, 0);
                            Console.Write("■");
                            Console.SetCursorPosition(i, height - 8);
                            Console.Write("■");
                            Console.SetCursorPosition(i, height - 2);
                            Console.Write("■");
                        }

                        for (int i = 0; i < height - 2; i++)
                        {
                            Console.SetCursorPosition(0, i);
                            Console.Write("■");
                            Console.SetCursorPosition(width - 2, i);
                            Console.Write("■");
                        }

                        #endregion

                        #region Boss属性

                        int bossPosX = (width - 2) / 2;
                        int bossPosY = (height - 1) / 2;
                        int bossHp = 100;
                        int boss_damageMin = 5;
                        int boss_damageMax = 14;
                        String bossIcon = "■";
                        ConsoleColor bossColor = ConsoleColor.Green;

                        #endregion

                        #region 玩家属性

                        int playerPosX = 4;
                        int playerPosY = 2;
                        int playerHp = 100;
                        int player_damageMin = 6;
                        int player_damageMax = 12;
                        String playerIcon = "●";
                        ConsoleColor playerColor = ConsoleColor.Yellow;
                        bool isFighting = false;
                        bool isOver = false;

                        #endregion

                        #region 公主

                        int princessPosX = 24;
                        int princessPosY = 5;
                        String princessIcon = "●";
                        ConsoleColor princessColor = ConsoleColor.DarkMagenta;

                        #endregion

                        while (true)
                        {
                            if (bossHp > 0)
                            {
                                Console.SetCursorPosition(bossPosX, bossPosY);
                                Console.ForegroundColor = bossColor;
                                Console.Write(bossIcon);
                            }
                            else
                            {
                                Console.SetCursorPosition(princessPosX, princessPosY);
                                Console.ForegroundColor = princessColor;
                                Console.Write(princessIcon);
                            }

                            if (playerHp > 0)
                            {
                                Console.SetCursorPosition(playerPosX, playerPosY);
                                Console.ForegroundColor = playerColor;
                                Console.Write(playerIcon);
                            }

                            Char playerInput = Console.ReadKey(true).KeyChar;

                            if (isOver)
                            {
                                break;
                            }

                            if (isFighting)
                            {
                                if (playerInput == 'j' || playerInput == 'J')
                                {
                                    if (playerHp <= 0)
                                    {
                                        gameOverInfo = "再接再厉";
                                        sceneId = 2;
                                        break;
                                    }
                                    else
                                    {
                                        int damage = random.Next(player_damageMin, player_damageMax);
                                        bossHp -= damage;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.SetCursorPosition(2, height - 6);
                                        Console.Write("                                              ");
                                        Console.SetCursorPosition(2, height - 6);
                                        Console.Write($"你对Boss造成了{damage}点伤害,Boss剩余血量为:{bossHp}");

                                        if (bossHp > 0)
                                        {
                                            damage = random.Next(boss_damageMin, boss_damageMax);
                                            playerHp -= damage;

                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(2, height - 5);
                                            Console.Write("                                              ");

                                            if (playerHp < 0)
                                            {
                                                Console.SetCursorPosition(2, height - 5);
                                                Console.Write("很遗憾你未能战胜Boss..., 按J键继续....");

                                                Console.SetCursorPosition(playerPosX, playerPosY);
                                                Console.Write("  ");
                                            }
                                            else
                                            {
                                                Console.SetCursorPosition(2, height - 5);
                                                Console.Write("Boss对你造成了" + damage + "点伤害,你的剩余血量为" + playerHp);
                                            }
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(2, height - 7);
                                            Console.Write("                                              ");
                                            Console.SetCursorPosition(2, height - 6);
                                            Console.Write("                                              ");
                                            Console.SetCursorPosition(2, height - 5);
                                            Console.Write("                                              ");

                                            Console.SetCursorPosition(bossPosX, bossPosY);
                                            Console.Write("  ");
                                            isFighting = false;

                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.SetCursorPosition(2, height - 7);
                                            Console.Write("恭喜你战胜了Boss!");
                                            Console.SetCursorPosition(2, height - 6);
                                            Console.Write("快去营救公主, 按J键继续....");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.SetCursorPosition(playerPosX, playerPosY);
                                Console.Write("  ");

                                switch (playerInput)
                                {
                                    case 'w':
                                    case 'W':
                                        playerPosY -= 1;
                                        if (playerPosY < 1)
                                        {
                                            playerPosY = 1;
                                        }
                                        else if (playerPosX == bossPosX && playerPosY == bossPosY && bossHp > 0)
                                        {
                                            playerPosY += 1;
                                        }
                                        else if (playerPosX == princessPosX && playerPosY == princessPosY &&
                                                 bossHp <= 0)
                                        {
                                            playerPosY += 1;
                                        }

                                        break;
                                    case 's':
                                    case 'S':
                                        playerPosY += 1;
                                        if (playerPosY > height - 9)
                                        {
                                            playerPosY = height - 9;
                                        }
                                        else if (playerPosX == bossPosX && playerPosY == bossPosY && bossHp > 0)
                                        {
                                            playerPosY -= 1;
                                        }
                                        else if (playerPosX == princessPosX && playerPosY == princessPosY &&
                                                 bossHp <= 0)
                                        {
                                            playerPosY -= 1;
                                        }

                                        break;
                                    case 'a':
                                    case 'A':
                                        playerPosX -= 2;
                                        if (playerPosX < 2)
                                        {
                                            playerPosX = 2;
                                        }
                                        else if (playerPosX == bossPosX && playerPosY == bossPosY && bossHp > 0)
                                        {
                                            playerPosX += 2;
                                        }
                                        else if (playerPosX == princessPosX && playerPosY == princessPosY &&
                                                 bossHp <= 0)
                                        {
                                            playerPosX += 2;
                                        }

                                        break;
                                    case 'd':
                                    case 'D':
                                        playerPosX += 2;
                                        if (playerPosX > width - 4)
                                        {
                                            playerPosX = width - 4;
                                        }
                                        else if (playerPosX == bossPosX && playerPosY == bossPosY && bossHp > 0)
                                        {
                                            playerPosX -= 2;
                                        }
                                        else if (playerPosX == princessPosX && playerPosY == princessPosY &&
                                                 bossHp <= 0)
                                        {
                                            playerPosX -= 2;
                                        }

                                        break;

                                    case 'j':
                                    case 'J':

                                        if ((playerPosX == bossPosX && playerPosY == bossPosY + 1 ||
                                             playerPosX == bossPosX && playerPosY == bossPosY - 1 ||
                                             playerPosX == bossPosX - 2 && playerPosY == bossPosY ||
                                             playerPosX == bossPosX + 2 && playerPosY == bossPosY) && bossHp > 0)
                                        {
                                            Console.SetCursorPosition(2, height - 7);
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write("发现Boss准备战斗, 按J键继续....");
                                            Console.SetCursorPosition(2, height - 6);
                                            Console.Write("玩家当前血量为" + playerHp);
                                            Console.SetCursorPosition(2, height - 5);
                                            Console.Write("Boss当前血量为" + bossHp);
                                            isFighting = true;
                                        }


                                        if ((playerPosX == princessPosX && playerPosY == princessPosY + 1 ||
                                             playerPosX == princessPosX && playerPosY == princessPosY - 1 ||
                                             playerPosX == princessPosX - 2 && playerPosY == princessPosY ||
                                             playerPosX == princessPosX + 2 && playerPosY == princessPosY) &&
                                            bossHp <= 0)
                                        {
                                            gameOverInfo = "英雄救美";
                                            sceneId = 2;
                                            isOver = true;
                                        }

                                        break;
                                }
                            }
                        }

                        break;
                    case 2:
                        bool gameOver = false;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.SetCursorPosition(width / 2 - 4, 8);
                        Console.Write("GameOver");
                        Console.SetCursorPosition(width / 2 - 4, 7);
                        Console.Write(gameOverInfo);

                        while (true)
                        {
                            Console.ForegroundColor = sceneIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.SetCursorPosition(width / 2 - 5, 12);
                            Console.Write("回到主界面");

                            Console.ForegroundColor = sceneIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.SetCursorPosition(width / 2 - 4, 14);
                            Console.Write("结束游戏");

                            char c = Console.ReadKey(true).KeyChar;
                            switch (c)
                            {
                                case 'w':
                                case 'W':
                                    sceneIndex = 0;
                                    break;

                                case 's':
                                case 'S':
                                    sceneIndex = 1;
                                    break;

                                case 'j':
                                case 'J':
                                    if (sceneIndex == 0)
                                    {
                                        sceneId = 0;
                                        gameOver = true;
                                    }
                                    else
                                    {
                                        Environment.Exit(0);
                                    }

                                    break;
                            }

                            if (gameOver) break;
                        }

                        break;
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}