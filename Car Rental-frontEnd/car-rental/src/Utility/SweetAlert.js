import Swal from "sweetalert2";

function ErrorBody(text, title) {
  return (
    Swal.fire({
      title: title || "Error!",
      text: text || "an error occurred.",
      icon: "error"
    })
  )
}

async function Sure(title, text, confirmButtonText) {
  let Result = await Swal.fire({
    title: title || "Are you sure?",
    text: text || "BMW has been selected and the rent per day is $13",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: confirmButtonText || "Yes, select it!"
  })
 return Result.isConfirmed
}


function ProcessSuccessful() {
  Swal.fire({
    position: "top-end",
    icon: "success",
    title: "The operation was successful",
    showConfirmButton: false,
    timer: 1500
  });
}

async function SureWithFire(title, text, confirmButtonText, titleThen, textThen) {
  let Result = await Swal.fire({
    title: title || "Are you sure?",
    text: text || "This process cannot be undone!",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: confirmButtonText || "Yes, I am sure!"
  })

  if (Result.isConfirmed) {
    await  Swal.fire({
      title: titleThen || "Booked!",
      text: textThen || "Your Item has been Booked.",
      icon: "success"

    })
  }
  // await Result.then((result) => {
  //   if (result.isConfirmed) {
  //    ;
  //   }
  // })

  return Result.isConfirmed
}


export { Sure, ErrorBody,SureWithFire ,ProcessSuccessful};