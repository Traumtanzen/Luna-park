﻿using static BlackJack.CardsEnums;

namespace BlackJack
{
    public class Card
    {
        public Suit Suit { get; set; }
        public Face Face { get; set; }
        public int Value { get; set; }
    }
}
