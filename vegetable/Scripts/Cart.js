
//用來接傳入的購物車
var carts;
var itemVue;
$( function () {
    GetCart();

} );

function GetCart() {
    $.ajax( {
        type: "Get",
        url: "/FrontEnd/CartItems",
        dataType: "text",
        success: function ( response ) {
            if ( response != "" ) {
                carts = response;
                CountItems( carts );
            }
        }
    } );
}

//計算購物車內所有物品數量
function CountItems( carts ) {
    carts = JSON.parse( carts );
    let counts = 0;
    carts.forEach( item => counts += item.Quantity )
    ChangeCartNumber( counts );
    //BindingVue( counts );
}

更改購物車數目
function ChangeCartNumber( counts ) {
    $( '.cartItems' ).each( function ( index, element ) {
        $( element ).text( `${counts}` );
    } );
}

////vue
//function BindingVue( counts ) {
//    itemVue = new Vue( {
//        el: ".cartItems",
//        data: { counts: counts }
//    } )
//}