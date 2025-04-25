window.addEventListener('DOMContentLoaded', () => {
    const container = document.getElementById('tableContainer');
    const stored = localStorage.getItem('calcResult');
    if (!stored) {
      container.innerHTML = '<p class="text-danger">Nincs elérhető eredmény. Kérlek, futtasd újra a kalkulátort.</p>';
      return;
    }
  
    // Display the raw JSON string exactly as stored
    // Wrap in <pre> to preserve formatting
    container.innerHTML = `<pre>${stored}</pre>`;
  
    // Optionally clear it so next visit starts fresh
    localStorage.removeItem('calcResult');
  });