
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TradeLastDayTradeLastDay
{

    private string lVal18AFCField;

    private decimal priceFirstField;

    private uint dEvenField;

    private ulong insCodeField;

    private string lVal30Field;

    private decimal pClosingField;

    private decimal pDrCotValField;

    private ushort zTotTranField;

    private uint qTotTran5JField;

    private decimal qTotCapField;

    private decimal priceChangeField;

    private decimal priceMinField;

    private decimal priceMaxField;

    private decimal priceYesterdayField;

    private byte lastField;

    private uint hEvenField;

    private string idField;

    private ushort rowOrderField;

    /// <remarks/>
    public string LVal18AFC
    {
        get
        {
            return this.lVal18AFCField;
        }
        set
        {
            this.lVal18AFCField = value;
        }
    }

    /// <remarks/>
    public decimal PriceFirst
    {
        get
        {
            return this.priceFirstField;
        }
        set
        {
            this.priceFirstField = value;
        }
    }

    /// <remarks/>
    public uint DEven
    {
        get
        {
            return this.dEvenField;
        }
        set
        {
            this.dEvenField = value;
        }
    }

    /// <remarks/>
    public ulong InsCode
    {
        get
        {
            return this.insCodeField;
        }
        set
        {
            this.insCodeField = value;
        }
    }

    /// <remarks/>
    public string LVal30
    {
        get
        {
            return this.lVal30Field;
        }
        set
        {
            this.lVal30Field = value;
        }
    }

    /// <remarks/>
    public decimal PClosing
    {
        get
        {
            return this.pClosingField;
        }
        set
        {
            this.pClosingField = value;
        }
    }

    /// <remarks/>
    public decimal PDrCotVal
    {
        get
        {
            return this.pDrCotValField;
        }
        set
        {
            this.pDrCotValField = value;
        }
    }

    /// <remarks/>
    public ushort ZTotTran
    {
        get
        {
            return this.zTotTranField;
        }
        set
        {
            this.zTotTranField = value;
        }
    }

    /// <remarks/>
    public uint QTotTran5J
    {
        get
        {
            return this.qTotTran5JField;
        }
        set
        {
            this.qTotTran5JField = value;
        }
    }

    /// <remarks/>
    public decimal QTotCap
    {
        get
        {
            return this.qTotCapField;
        }
        set
        {
            this.qTotCapField = value;
        }
    }

    /// <remarks/>
    public decimal PriceChange
    {
        get
        {
            return this.priceChangeField;
        }
        set
        {
            this.priceChangeField = value;
        }
    }

    /// <remarks/>
    public decimal PriceMin
    {
        get
        {
            return this.priceMinField;
        }
        set
        {
            this.priceMinField = value;
        }
    }

    /// <remarks/>
    public decimal PriceMax
    {
        get
        {
            return this.priceMaxField;
        }
        set
        {
            this.priceMaxField = value;
        }
    }

    /// <remarks/>
    public decimal PriceYesterday
    {
        get
        {
            return this.priceYesterdayField;
        }
        set
        {
            this.priceYesterdayField = value;
        }
    }

    /// <remarks/>
    public byte Last
    {
        get
        {
            return this.lastField;
        }
        set
        {
            this.lastField = value;
        }
    }

    /// <remarks/>
    public uint HEven
    {
        get
        {
            return this.hEvenField;
        }
        set
        {
            this.hEvenField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "urn:schemas-microsoft-com:xml-msdata")]
    public ushort rowOrder
    {
        get
        {
            return this.rowOrderField;
        }
        set
        {
            this.rowOrderField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public class TradeLastDay
{

    private TradeLastDayTradeLastDay[] tradeLastDay1Field;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("TradeLastDay")]
    public TradeLastDayTradeLastDay[] TradeLastDay1
    {
        get
        {
            return this.tradeLastDay1Field;
        }
        set
        {
            this.tradeLastDay1Field = value;
        }
    }
}


