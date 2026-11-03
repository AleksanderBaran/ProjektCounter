using System.Text.Json;

namespace Counter;

public partial class MainPage : ContentPage
{
    private List<CounterItemModel> list = new List<CounterItemModel>();

    public MainPage()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        string json = Preferences.Get("DataJson", string.Empty);

        if (!string.IsNullOrEmpty(json))
        {
            try
            {
                var loaded = JsonSerializer.Deserialize<List<CounterItemModel>>(json);
                if (loaded != null)
                {
                    list = loaded;
                    foreach (var item in list)
                    {
                        MakeCounter(item, false);
                    }
                }
            }
            catch { }
        }
    }

    private void SaveData()
    {
        string json = JsonSerializer.Serialize(list);
        Preferences.Set("DataJson", json);
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        string name = string.IsNullOrWhiteSpace(NameInput.Text) ? "Licznik" : NameInput.Text;
        int startVal = 0;
        int.TryParse(StartInput.Text, out startVal);

        var newItem = new CounterItemModel
        {
            Name = name,
            InitialValue = startVal,
            Value = startVal 
        };

        list.Add(newItem);
        MakeCounter(newItem, true);

        NameInput.Text = string.Empty;
        StartInput.Text = "0";
    }


    private void MakeCounter(CounterItemModel data, bool save)
    {
        Frame frame = new Frame
        {
            Padding = 12,
            CornerRadius = 8,
            HasShadow = true
        };

        HorizontalStackLayout row = new HorizontalStackLayout
        {
            Spacing = 10,
            VerticalOptions = LayoutOptions.Center
        };

        Label nameLbl = new Label
        {
            Text = data.Name,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 120
        };

        Button minusBtn = new Button { Text = "-", WidthRequest = 45, FontSize = 18 };

        Label valLbl = new Label
        {
            Text = data.Value.ToString(),
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 50
        };

        Button plusBtn = new Button { Text = "+", WidthRequest = 45, FontSize = 18 };

        minusBtn.Clicked += (s, e) =>
        {
            data.Value--;
            valLbl.Text = data.Value.ToString();
            SaveData();
        };

        plusBtn.Clicked += (s, e) =>
        {
            data.Value++;
            valLbl.Text = data.Value.ToString();
            SaveData(); 
        };

        row.Children.Add(nameLbl);
        row.Children.Add(minusBtn);
        row.Children.Add(valLbl);
        row.Children.Add(plusBtn);

        frame.Content = row;
        CountersContainer.Children.Add(frame);

        if (save)
        {
            SaveData();
        }
    }
}