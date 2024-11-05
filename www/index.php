<?php
?>
<!DOCTYPE html>
<html>
<head>
    <title>Métricas</title>
</head>
<body>
<h1>Métricas</h1>
<div id="metrics"></div>

<script>
    async function fetchMetrics() {
        try {
            const response = await fetch('http://thrire.com/metrics.php');
            const data = await response.json();
            document.getElementById('metrics').innerHTML = JSON.stringify(data, null, 2);
        } catch (error) {
            document.getElementById('metrics').innerHTML = 'Erro ao buscar as métricas';
        }
    }

    fetchMetrics();
</script>
</body>
</html>

