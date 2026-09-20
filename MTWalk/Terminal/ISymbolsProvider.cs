namespace MTWalk.Terminal;

public interface ISymbolsProvider
{
    Symbol[] GetSymbols();
}

public class SymbolsProvider : ISymbolsProvider
{
    public Symbol[] GetSymbols() =>
    [
        new SymbolCategory("Forex", "Forex",
        [
            new Symbol("AUDCAD", "Australian Dollar vs Canadian Dollar"),
            new Symbol("AUDCHF", "Australian Dollar vs Swiss Franc"),
            new Symbol("AUDJPY", "Australian Dollar vs Japanese Yen"),
            new Symbol("AUDNZD", "Australian Dollar vs New Zealand Dollar"),
            new Symbol("AUDSGD", "Australian Dollar vs Singapore Dollar"),
            new Symbol("AUDUSD", "Australian Dollar vs US Dollar"),
            new Symbol("CADCHF", "Canadian Dollar vs Swiss Franc"),
            new Symbol("CADJPY", "Canadian Dollar vs Japanese Yen"),
            new Symbol("CHFJPY", "Swiss Frank vs Japanese Yen"),
            new Symbol("EURAUD", "Euro vs Australian Dollar"),
            new Symbol("EURCAD", "Euro vs Canadian Dollar"),
            new Symbol("EURCHF", "Euro vs Swiss Franc"),
            new Symbol("EURGBP", "Euro vs Great Britain Pound"),
            new Symbol("EURHKD", "Euro vs Hong Kong Dollar"),
            new Symbol("EURHUF", "Euro vs Hungarian Forint"),
            new Symbol("EURJPY", "Euro vs Japanese Yen"),
            new Symbol("EURNOK", "Euro vs Norwegian Kroner"),
            new Symbol("EURNZD", "Euro vs New Zealand Dollar"),
            new Symbol("EURSGD", "Euro vs Singapore Dollar"),
            new Symbol("EURUSD", "Euro vs US Dollar"),
            new Symbol("GBPAUD", "Great Britain Pound vs Australian Dollar"),
            new Symbol("GBPCAD", "Great Britain Pound vs Canadian Dollar"),
            new Symbol("GBPCHF", "Great Britain Pound vs Swiss Franc"),
            new Symbol("GBPJPY", "Great Britain Pound vs Japanese Yen"),
            new Symbol("GBPNZD", "Great Britain Pound vs New Zealand Dollar"),
            new Symbol("GBPSGD", "Great Britain Pound vs Singapore Dollar"),
            new Symbol("GBPUSD", "Great Britain Pound vs US Dollar"),
            new Symbol("MXNJPY", "Mexican Peso vs Japanese Yen"),
            new Symbol("NOKJPY", "Norwegian Kroner vs Japanese Yen"),
            new Symbol("NZDCAD", "New Zealand Dollar vs Canadian Dollar"),
            new Symbol("NZDCHF", "New Zealand Dollar vs Swiss Franc"),
            new Symbol("NZDJPY", "New Zealand Dollar vs Japanese Yen"),
            new Symbol("NZDSGD", "New Zealand Dollar vs Singapore Dollar"),
            new Symbol("NZDUSD", "New Zealand Dollar vs US Dollar"),
            new Symbol("SGDJPY", "Singapore Dollar vs Japanese Yen"),
            new Symbol("USDCAD", "US Dollar vs Canadian Dollar"),
            new Symbol("USDCHF", "US Dollar vs Swiss Franc"),
            new Symbol("USDCNH", "US Dollar vs Chinese Yuan Renminbi"),
            new Symbol("USDDKK", "US Dollar vs Danish Krone"),
            new Symbol("USDHKD", "US Dollar vs Hong Kong Dollar"),
            new Symbol("USDHUF", "US Dollar vs Hungarian Forint"),
            new Symbol("USDJPY", "US Dollar vs Japanese Yen"),
            new Symbol("USDMXN", "US Dollar vs Mexican Peso"),
            new Symbol("USDNOK", "US Dollar vs Norwegian Kroner"),
            new Symbol("USDSEK", "US Dollar vs Swedish Krona"),
            new Symbol("USDSGD", "US Dollar vs Singapore Dollar"),
            new Symbol("USDZAR", "US Dollar vs South African Rand"),
            new Symbol("ZARJPY", "South African Rand vs Japanese Yen")
        ]),

        new SymbolCategory("Commodities", "Commodities",
        [
            new Symbol("UKOUSD", "Crude Oil Brent Cash"),
            new Symbol("USOUSD", "West Texas Intermediate Crude Oil cash"),
            new Symbol("XAGUSD", "Silver vs US-Dollar"),
            new Symbol("XAUUSD", "Gold vs US-Dollar"),
            new Symbol("XPTUSD", "Platinum vs US-Dollar")
        ]),

        new SymbolCategory("Indices", "Indices",
        [
            new Symbol("AUS200", "Australia 200 Cash Index"),
            new Symbol("EUSTX50", "Euro 50 Cash index"),
            new Symbol("FRA40", "France 40 Cash Index"),
            new Symbol("GER30", "Germany 40 Cash index"),
            new Symbol("HK50", "Hong Kong"),
            new Symbol("JP225", "Japan 225 Index"),
            new Symbol("NDX100", "NAS100"),
            new Symbol("NTH25", "Netherlands 25 Cash Index"),
            new Symbol("SPX500", "SPX500"),
            new Symbol("SWI20", "Switzerland 20 Cash Index"),
            new Symbol("UK100", "UK 100 Cash Index"),
            new Symbol("US2000", "Russell 2000 Index"),
            new Symbol("US30", "Dow Jones 30"),
            new Symbol("VIX", "Volatility Index")
        ]),

        new SymbolCategory("Cryptocurrencies", "Cryptocurrencies",
        [
            new Symbol("ADAUSD", "Cardano vs US Dollar"),
            new Symbol("BTCUSD", "Bitcoin vs US Dollar"),
            new Symbol("DOGUSD", "Doge Coin vs US Dollar"),
            new Symbol("ETHUSD", "Ethereum vs US Dollar"),
            new Symbol("LNKUSD", "Link vs US Dollar"),
            new Symbol("LTCUSD", "Litecoin vs US Dollar"),
            new Symbol("XLMUSD", "Stellar vs US Dollar"),
            new Symbol("XMRUSD", "Monero vs US Dollar"),
            new Symbol("XRPUSD", "Ripple vs US dollar")
        ]),

        new SymbolCategory("Stock CFD", "Stock CFD",
        [
            new SymbolCategory("Equities US", "Equities US",
            [
                new Symbol("AAPL", "Apple, Spot CFD"),
                new Symbol("AMD", "Advanced Micro Devices, Spot CFD"),
                new Symbol("AMZN", "Amazon Com, Spot CFD"),
                new Symbol("BABA", "Alibaba Group, Spot CFD"),
                new Symbol("GOOG", "Alphabet Class C, Spot CFD"),
                new Symbol("INTC", "Intel Corp, Spot CFD"),
                new Symbol("KO", "Coca-Cola, Spot CFD"),
                new Symbol("META", "Meta Platforms, Spot CFD"),
                new Symbol("MSFT", "Microsoft, Spot CFD"),
                new Symbol("NFLX", "Netflix, Spot CFD"),
                new Symbol("NKE", "Nike, Spot CFD"),
                new Symbol("NVDA", "NVIDIA, Spot CFD"),
                new Symbol("SPCX", "SpaceX, Spot CFD"),
                new Symbol("TSLA", "Tesla, Spot CFD")
            ]),
            new SymbolCategory("Equities EU", "Equities EU",
            [
                new Symbol("ADS", "Adidas AG, Spot CFD"),
                new Symbol("BAYN", "Bayer, Spot CFD"),
                new Symbol("BMW", "BMW AG, Spot CFD"),
                new Symbol("MBG", "Mercedes Benz Group AG, Spot CFD"),
                new Symbol("MC", "LVMH, Spot CFD"),
                new Symbol("VOW3", "Volkswagen, Spot CFD")
            ])
        ])
    ];
}

public record Symbol(
    string Name, 
    string Description);

public record SymbolCategory(
    string Name, 
    string Description, 
    Symbol[] Children) 
    : Symbol(Name, Description);