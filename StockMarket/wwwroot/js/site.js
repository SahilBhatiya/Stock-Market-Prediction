const form = $("#DisplayStock");

const mapToArray = (arr = []) => {
    const res = [];
    arr.forEach(function (obj, index) {
        const key1 = Object.keys(obj)[1];
        const key2 = Object.keys(obj)[2];
        res.push([obj[key1], obj[key2]]);
    });
    return res;
};

const Volume = (arr = []) => {
    const res = [];
    arr.forEach(function (obj, index) {
        const key1 = Object.keys(obj)[1];
        const key2 = Object.keys(obj)[3];
        res.push([obj[key1], obj[key2]]);
    });
    return res;
};



form.submit(function (event) {
    event.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    const data = form.serializeArray();
    $.ajax({
        type: "POST",
        url: actionUrl,
        data: data,
        success: function (receviedData) {
            receviedData = JSON.parse(receviedData)


            let result = Object.values(receviedData);
            var finalArray1 = mapToArray(result)
            var finalArray2 = Volume(result)
/*            console.log(finalArray[0][0])

            $("#DisplayData").text(finalArray)*/

            var options = {
                series: [{
                    data: finalArray1
                }],
                chart: {
                    id: 'area-datetime',
                    type: 'area',
                    height: 350,
                    zoom: {
                        autoScaleYaxis: true
                    },
                    stroke: {
                        curve: 'smooth',
                    }
                },
                grid: {
                    show : false
                },
                dataLabels: {
                    enabled: false
                },
                markers: {
                    size: 0,
                    style: 'hollow',
                },
                xaxis: {
                    type: 'datetime',
                    tickAmount: 6,
                },
                tooltip: {
                    x: {
                        format: 'dd MMM yyyy'
                    }
                },
                fill: {
                    type: 'gradient',
                    gradient: {
                        shadeIntensity: 1,
                        opacityFrom: 0.4,
                        opacityTo: 0.9,
                        stops: [0, 100]
                    }
                },
            };

            var chart = new ApexCharts(document.querySelector("#chart"), options);
            chart.render();

            options = {
                series: [{
                    data: finalArray2
                }],
                chart: {
                    id: 'area-datetime',
                    type: 'area',
                    height: 350,
                    zoom: {
                        autoScaleYaxis: true
                    },
                    stroke: {
                        curve: 'smooth',
                    }
                },
                grid: {
                    show: false
                },
                dataLabels: {
                    enabled: false
                },
                markers: {
                    size: 0,
                    style: 'hollow',
                },
                xaxis: {
                    type: 'datetime',
                    tickAmount: 6,
                },
                tooltip: {
                    x: {
                        format: 'dd MMM yyyy'
                    }
                },
                fill: {
                    type: 'gradient',
                    gradient: {
                        shadeIntensity: 1,
                        opacityFrom: 0.4,
                        opacityTo: 0.9,
                        stops: [0, 100]
                    }
                },
            };

            chart = new ApexCharts(document.querySelector("#Volume"), options);
            chart.render();
        }
    })
});


let sharePrice = 0, crrPrice = 0, TotalQty = 1, TotalPrice, ProfitPerShare = 0, lowPriceRange = 0, highPriceRange = 0, ProfitPercent = 0, TotalProfit = 0

function getLastAverage() {
    $.ajax({
        type: "POST",
        url: "/Home/Last14DaysVolumeAvg",
        success: function (data) {
            sharePrice = data
        
        }
    })
}



function updatePercent() {
    const crrPrice2 = TotalPrice = crrPrie = $("#current").val()
    highPriceRange = sharePrice * 1.03
    lowPriceRange = sharePrice * 0.97

    

    const totalDiff = lowPriceRange + highPriceRange
    ProfitPercent = (sharePrice / totalDiff) * 100
    TotalProfit = (sharePrice - crrPrice2) * TotalQty

    UpdateTotalPrice()

}

function updateQty() {
    TotalPrice = crrPrie = $("#current").val()

    TotalQty = $("#qty").val()

    console.log(crrPrice)

    const totalDiff = lowPriceRange - crrPrice + highPriceRange - crrPrice
    ProfitPercent = (highPriceRange / totalDiff) * 100


    TotalPrice = TotalPrice * TotalQty
    TotalProfit = ((sharePrice - crrPrice) * TotalQty) - TotalPrice
    highPriceRange = sharePrice * 1.03
    lowPriceRange = sharePrice * 0.97


    UpdateTotalQty()
    UpdateTotalPrice()
    UpdateProfitPerShare()
    UpdateNextPrice()
    UpdatePriceRange()
    UpdateProfitChance()
    UpdateTotalProfit()
}

getLastAverage()

form.submit()


function UpdateTotalQty() {
    $("#TotalQty").text(TotalQty)
}

function UpdateTotalPrice() {
    $("#TotalPrice").text(TotalPrice)
}

function UpdateProfitPerShare() {
    $("#ProfitPerShare").text((sharePrice - crrPrie))
}

function UpdateNextPrice() {
    $("#NextPrice").text(sharePrice)
}

function UpdatePriceRange() {
    $("#PriceRange").text(highPriceRange + " - " + lowPriceRange)
}

function UpdateProfitChance() {
    $("#ProfitChance").text(ProfitPercent + "%")
}

function UpdateTotalProfit() {
    $("#TotalProfit").text(TotalProfit)
}