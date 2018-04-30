 public float clicksNeeded;  // The number of clicks needed to break this block
    public float reward;  // The number of points the player gets for breaking this block
    public float multiplier;
    public int xCord, yCord;
    public bool isClick;
    public bool selected;
    public Color color;
    public Block nextBlock;

    Block(int clicks, int reward)
    {
        this.clicksNeeded = clicks;
        this.reward = reward;
        this.multiplier = 1;
    }

    // Use this for initialization
    void Start () {
        clicksNeeded = 5;
        reward = 50;
        multiplier = 1;
        selected = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(clicksNeeded <= 0)
        {
            Object.Destroy(gameObject);
            GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().addToCurrency(reward);
            GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().currentBlock = GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().getNextBlock();
        }

        if(selected)
        {
            this.GetComponent<Outline>().enabled = true;
            this.GetComponent<Renderer>().material.color = color;

        }
        else
        {
            this.GetComponent<Outline>().enabled = false;
            this.GetComponent<Renderer>().material.color = color;
        }
	}

    // Getters
    public float getReward() { return reward; }
    public float getClicksNeeded() { return clicksNeeded; }
    public void setReward()
    {
        if (color == Color.green) { reward = 50; }
        else if (color == Color.blue) { reward = 75; }
        else if (color == Color.red) { reward = 150; }
        else if (color == Color.gray) { reward = 200; }
        else if (color == Color.magenta) { reward = 300; }
        else { reward = 50; }
    }
    public void setClicks(int clicks) { this.clicksNeeded = clicks; }

    void OnMouseDown()
    {
        if (isClick) 
        {
            if(!selected)
            {
                selected = true;
                GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().currentBlock = this;
            }
            else
            {
                this.clicksNeeded--;
                GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().addToCurrency(1 * (int)this.multiplier);
            }
        }
    }

    public void hit()
    {
        clicksNeeded -= 1 * (int)this.multiplier;
        GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().addToCurrency(1 * (int)this.multiplier);
    }

    public Vector2 getLocation()
    {
        return new Vector2(xCord, yCord);
    }