window.onload = function () {
    const calculatorForm = document.querySelector('.calculator');

    calculatorForm.addEventListener('submit', function (e) {
        const checkbox = e.target.querySelector('#js-eval');

        if (checkbox.checked) {
            e.preventDefault();
            const expression = e.target.querySelector('#expression').value;
            const resultEl = e.target.querySelector('.result');
            resultEl.textContent = '';
            clearErrors(e.target);

            if (!isExpressionValid(expression)) {
                showError(e.target);
                return;
            }

            let result;
            try {
                result = eval(expression);
            } catch {
                showError(e.target);
            }
            resultEl.textContent = result;
        }
    });

    function isExpressionValid(expression) {
        const pattern = /^[\d\+\-\*\/\(\)\.\s]+$/;
        return pattern.test(expression);
    }

    function showError(form) {
        const error = document.createElement('span');
        error.textContent = "Invalid expression.";
        form.querySelector('.errors').appendChild(error);
    }

    function clearErrors(form) {
        form.querySelector('.errors').innerHTML = '';
    }
};