document.addEventListener('DOMContentLoaded', function () {
  const ctx = document.getElementById('myChart');

  new Chart(ctx, {
    type: 'line',
    data: {
      labels: [
        'Sunday',
        'Monday',
        'Tuesday',
        'Wednesday',
        'Thursday',
        'Friday',
        'Saturday'
      ],
      datasets: [{
         label: "doanh thu trong tuần",
        data: [
          15339000,
          21345000,
          18483000,
          24003000,
          23489000,
          24092000,
          5000000,
        ],
        lineTension: 0,
        backgroundColor: 'transparent',
        borderColor: '#007bff',
        borderWidth: 4,
        pointBackgroundColor: '#007bff'
      }]
    },
    options: {
      scales: {
        y: {
          beginAtZero: true,
          ticks: {
            callback: function (value, index, values) {
              return value / 1000000 + 'tr';
            }
          }
        }
      }
    }
  })
});