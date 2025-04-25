
const API_BASE  = 'http://localhost:5153';  
const THRESHOLD = 350000;

window.addEventListener('DOMContentLoaded', () => {
  const form = document.getElementById('calcForm');
  form.addEventListener('submit', async e => {
    e.preventDefault();

    const unitPrice = parseFloat(document.getElementById('unitPrice').value);
    const rows = document.getElementById('matrixRows')
      .value.trim()
      .split(/\r?\n/);

    // Fetch to backend
    const resp = await fetch(`${API_BASE}/api/electricitybill/calculate`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ unitPrice, matrixRows: rows })
    });
    if (!resp.ok) {
      alert('Hiba a szerveren: ' + resp.status);
      return;
    }
    const data = await resp.json();

    // Save result and redirect
    localStorage.setItem('calcResult', JSON.stringify({ years: rows[0].split(','), data }));
    window.location.href = 'results.html';
  });
});

  