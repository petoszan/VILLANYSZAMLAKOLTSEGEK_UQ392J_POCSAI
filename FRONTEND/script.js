document.getElementById('calcForm').addEventListener('submit', async e => {
    e.preventDefault();
    const unitPrice = parseFloat(document.getElementById('unitPrice').value);
    const rows = document.getElementById('matrixRows').value.trim().split(/\r?\n/);
    const resp = await fetch('https://localhost:7048/api/electricitybill/calculate', {
      method: 'POST', headers: {'Content-Type':'application/json'},
      body: JSON.stringify({unitPrice, matrixRows: rows})
    });
    if (!resp.ok) return alert('Hiba: ' + resp.status);
    const data = await resp.json();
    // Tároljuk az eredményt localStorage-ben
    localStorage.setItem('calcResult', JSON.stringify({years: rows[0].split(','), data}));
    // Átirányítás az eredmény lapra
    window.location.href = 'results.html';
  });