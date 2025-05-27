const ctx = document.getElementById('faturamentoChart').getContext('2d');
const faturamentoChart = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: ['1º Trim', '2º Trim', '3º Trim', '4º Trim'],
        datasets: [{
            label: 'Faturamento por Trimestre',
            data: [10000, 15000, 13000, 16000],
            backgroundColor: '#7CFC00',
            borderRadius: 8,
            barThickness: 50,
        }]
    },
    options: {
        responsive: true,
        plugins: {
            legend: {display: false}
        },
        scales: {
            y: {
                beginAtZero: true,
                ticks: {
                    callback: function (value) {
                        return 'R$ ' + value.toLocaleString('pt-BR');
                    }
                }
            }
        }
    }
});
