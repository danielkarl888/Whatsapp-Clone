//when the page is ready -
$(function () {
    $('form').submit(async e => {
        e.preventDefault();
        const q = $('#search').val();

         $('tbody').load('/Ranks/Index2?query=' + q);

    })
})