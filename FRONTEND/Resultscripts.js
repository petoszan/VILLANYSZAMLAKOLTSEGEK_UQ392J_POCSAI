// Resultscripts.js
// Read from localStorage and render table of monthly and yearly costs

window.addEventListener('DOMContentLoaded', () => {
  const container = document.getElementById('tableContainer');
  const storedJson = localStorage.getItem('calcResult');
  if (!storedJson) {
    container.innerHTML = '<p class="text-danger">Nincs elérhető eredmény. Kérlek, futtasd újra a kalkulátort.</p>';
    return;
  }

  let obj;
  try {
    obj = JSON.parse(storedJson);
  } catch (e) {
    container.innerHTML = '<p class="text-danger">Hibás adat: nem feldolgozható JSON.</p>';
    return;
  }

  const years = obj.years;
  const yearlyCosts = obj.data.yearlyCosts;
  const monthlyCosts = obj.data.monthlyCosts;

  const months = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];

  // Build table HTML
  let html = '<table class="table table-bordered text-center"><thead><tr><th>Hónap</th>';
  years.forEach(yr => {
    html += `<th>${yr}</th>`;
  });
  html += '</tr></thead><tbody>';

  months.forEach((m, mi) => {
    html += `<tr><td class="fw-bold">${m}</td>`;
    years.forEach(yr => {
      const key = `${yr}-${String(mi+1).padStart(2, '0')}`;
      const cost = monthlyCosts[key];
      html += `<td>${cost.toFixed(2)}</td>`;
    });
    html += '</tr>';
  });

  html += '</tbody><tfoot><tr><td>Összesen (Ft)</td>';
  years.forEach(yr => {
    const total = yearlyCosts[yr];
    html += `<td>${total.toFixed(2)}</td>`;
  });
  html += '</tr></tfoot></table>';

  container.innerHTML = html;
  localStorage.removeItem('calcResult');
});
