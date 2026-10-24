# MTWalk: MetaTrader 5 Walk-Forward Optimizer

MTWalk is a robust C#/.NET WPF desktop application designed to orchestrate automated backtesting and Walk-Forward Optimization (WFO) for MetaTrader 5. It bypasses the limitations of the native MT5 Strategy Tester by driving the terminal via its Command-Line Interface (CLI), extracting data from generated HTML reports, and visualizing the results in a modern, responsive UI.

## 🚀 Features

* **Automated MT5 Execution:** Drives `terminal64.exe` headlessly (or minimized) using dynamically generated `.ini` configuration files.
* **Walk-Forward Orchestration:** Splits historical data into optimization and out-of-sample testing windows, automatically queueing and executing sequential backtests.
* **Dynamic Path Discovery:** Automatically resolves complex MT5 portable data folder paths by parsing the terminal's `origin.txt` hashes.
* **HTML Report Parsing:** Bypasses MT5's XML/HTML report limitations by programmatically parsing the "Deals" table using `HtmlAgilityPack` to extract granular time and balance data.
* **Professional Trading UI:**
* Mimics the native MT5 Toolbox with a collapsible `ExecutionBar` for tracking backtest progress.
* Advanced dual-Y-axis charting using **OxyPlot**, accurately recreating MT5's Balance/Equity step-lines and Deposit Load histograms.


* **Asynchronous & Decoupled:** Built on a clean MVVM architecture using `CommunityToolkit.Mvvm`, `IProgress<T>` for seamless background task reporting, and strict Dependency Injection.

## 🛠️ Tech Stack

* **Framework:** .NET (C#)
* **UI Presentation:** Windows Presentation Foundation (WPF) / XAML
* **Architecture:** MVVM (Model-View-ViewModel)
* **Libraries:**
* [`CommunityToolkit.Mvvm`](https://github.com/CommunityToolkit/dotnet) - Messaging and ViewModel infrastructure.
* [`OxyPlot.Wpf`](https://github.com/oxyplot/oxyplot) - High-performance financial charting.
* [`HtmlAgilityPack`](https://html-agility-pack.net/) - Report parsing and DOM traversal.



## 📸 Screenshots

| Strategy Tester View | Walk-Forward Execution |
| --- | --- |
| `![Tester](docs/images/tester_view.png)` | `![Execution](docs/images/execution_bar.png)` |

## ⚙️ Getting Started

### Prerequisites

* Windows 10 / 11
* .NET 8.0 SDK (or later)
* MetaTrader 5 Desktop Client installed natively.

### Installation

1. Clone the repository:
```bash
git clone https://github.com/nima-ghomri/MTWalk.git

```


2. Open the solution in Visual Studio.
3. Restore NuGet packages.
4. Build and run the `MTWalk.WPF` project.

## 🧠 Architecture Highlights

### The Execution Engine

MTWalk manages the MT5 terminal lifecycle via the `ProcessStartInfo` API. It suppresses startup sounds (`Enable=0` in the INI) and manages window states to prevent the terminal from stalling the optimization loop. Background tasks are tracked using `IProgress<WalkProgressReport>`, ensuring thread-safe UI updates without locking the WPF Dispatcher.

### Data Visualization

Unlike standard line charts, MTWalk utilizes OxyPlot's `StepSeries` to perfectly replicate the blocky, staircase visual of an MT5 balance curve, mapping historical trade closures directly to the timeline alongside a `LinearBarSeries` for margin load.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!
Feel free to check the [issues page](https://github.com/nima-ghomri/MTWalk/issues) if you want to contribute.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request *(Pro-tip: Use "Closes #IssueNumber" in your PR description to auto-resolve issues upon merge).*

## 📝 License

Distributed under the MIT License. See `LICENSE` for more information.
## ❤️ Support the Project

If MTWalk has saved you time analyzing your MetaTrader 5 optimizations, consider buying me a coffee to support future updates!

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/nimaghomri)

Or you can donate directly via:
- [PayPal.Me](https://paypal.me/nimaghomri)
- [Revolut](https://revolut.me/nima_ghomri)

---

**Author:** Nima Ghomri