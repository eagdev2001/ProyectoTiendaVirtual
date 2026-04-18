
(function () {
  const btn = document.getElementById('theme-toggle');
  if (!btn) return;

  const applyTheme = (t) => {
    document.documentElement.setAttribute('data-theme', t);
    btn.textContent = t === 'dark' ? '☀️' : '🌙';
  };

  const saved = localStorage.getItem('theme');
  const initial = saved || (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');
  applyTheme(initial);

  btn.addEventListener('click', () => {
    const next = document.documentElement.getAttribute('data-theme') === 'dark' ? 'light' : 'dark';
    applyTheme(next);
    localStorage.setItem('theme', next);
  });
})();

/*(function togglePassword() {
    const passwordInput = document.getElementById('passwordInput');
    if (!passwordInput) return;

    window.togglePassword = function () {
        const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
        passwordInput.setAttribute('type', type);
    };
})();*/