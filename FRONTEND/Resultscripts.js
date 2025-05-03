//Oldal formázása
const theme = localStorage.getItem('theme') || 'theme-default';
    document.body.classList.add(theme);



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
  const THRESHOLD = 350000;

  // Determine which years get discount
  const yearValues = years.map(y => yearlyCosts[y]);
  const discounted = years.map((y, i) => {
    if (i < 2) return false;
    return yearValues[i - 1] > THRESHOLD && yearValues[i - 2] > THRESHOLD;
  });

  const months = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];

  // Build table HTML
  let html = '<table class="table table-bordered text-center"><thead><tr><th>Hónap</th>';
  years.forEach((yr, i) => {
    if (discounted[i]) {
      html += `<th style="background-color:#d4edda;">${yr}</th>`;
    } else {
      html += `<th>${yr}</th>`;
    }
  });
  html += '</tr></thead><tbody>';

  months.forEach((m, mi) => {
    html += `<tr><td class="fw-bold">${m}</td>`;
    years.forEach((yr, i) => {
      const key = `${yr}-${String(mi + 1).padStart(2, '0')}`;
      const cost = monthlyCosts[key];
      if (discounted[i]) {
        html += `<td style="background-color:#d4edda;">${cost.toFixed(2)}</td>`;
      } else {
        html += `<td>${cost.toFixed(2)}</td>`;
      }
    });
    html += '</tr>';
  });

  // Footer with yearly totals
  html += '</tbody><tfoot><tr><td>Összesen (Ft)</td>';
  years.forEach((yr, i) => {
    const total = yearlyCosts[yr];
    if (discounted[i]) {
      html += `<td style="background-color:#d4edda;">${total.toFixed(2)}</td>`;
    } else {
      html += `<td>${total.toFixed(2)}</td>`;
    }
  });
  html += '</tr></tfoot></table>';

  container.innerHTML = html;
  localStorage.removeItem('calcResult');
});
