using System.Collections.ObjectModel;
using System.Text.Json;
using CounterProject.Models;

namespace CounterProject.Views;

public partial class MainPage : ContentPage
{
    ObservableCollection<CounterItemModel> countersList = new ObservableCollection<CounterItemModel>();

    public MainPage()
    {
        InitializeComponent();
        CountersCollection.ItemsSource = countersList;
        LoadData();
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        string inputName = NameInput.Text;

        if (string.IsNullOrEmpty(inputName))
        {
            inputName = "Nowy licznik";
        }

        CounterItemModel newCounter = new CounterItemModel();
        newCounter.Name = inputName;
        newCounter.Value = 0;

        countersList.Add(newCounter);
        NameInput.Text = "";
    }

    private void OnPlusClicked(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        CounterItemModel selectedCounter = (CounterItemModel)clickedButton.BindingContext;

        selectedCounter.Value = selectedCounter.Value + 1;
    }

    private void OnMinusClicked(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        CounterItemModel selectedCounter = (CounterItemModel)clickedButton.BindingContext;

        selectedCounter.Value = selectedCounter.Value - 1;
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        string jsonText = JsonSerializer.Serialize(countersList);
        Preferences.Set("saved_counters", jsonText);
    }

    private void LoadData()
    {
        string jsonText = Preferences.Get("saved_counters", "");

        if (jsonText != "")
        {
            var loadedList = JsonSerializer.Deserialize<ObservableCollection<CounterItemModel>>(jsonText);

            if (loadedList != null)
            {
                countersList = loadedList;
                CountersCollection.ItemsSource = countersList;
                return;
            }
        }

        CounterItemModel defaultCounter = new CounterItemModel();
        defaultCounter.Name = "Licznik Startowy";
        defaultCounter.Value = 0;

        countersList.Add(defaultCounter);
    }
}