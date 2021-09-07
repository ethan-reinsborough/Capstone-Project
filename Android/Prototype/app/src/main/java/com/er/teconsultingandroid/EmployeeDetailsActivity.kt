package com.er.teconsultingandroid

import android.content.Intent
import android.net.Uri
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.Toast
import androidx.appcompat.app.ActionBar
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import kotlinx.android.synthetic.main.activity_employee_details.*
import kotlinx.android.synthetic.main.activity_main.*
import org.json.JSONArray
import org.json.JSONObject
import java.lang.Exception

class EmployeeDetailsActivity : AppCompatActivity() {

    private var employeeId:String? = null //gotten from intent
    private val TAG = "POST_DETAILS_TAG"

    //action bar
    private lateinit var actionBar:ActionBar

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_employee_details)

        //init actionbar
        actionBar = supportActionBar!!

        actionBar.title = "Employee Details"

        actionBar.setDisplayHomeAsUpEnabled(true)
        actionBar.setDisplayShowHomeEnabled(true)

        //get the employee id
        employeeId = intent.getStringExtra("employeeId")

        loadEmployeeDetails()
    }

    private fun loadEmployeeDetails(){
        val url = "http://10.0.2.2:44349/api/Employees/${employeeId}"
        val stringRequest = StringRequest(Request.Method.GET, url, { response ->


                //got response as JSON Object
                val jsonObject = JSONObject(response)

                        val id = jsonObject.getInt("employeeId")
                        val firstName = jsonObject.getString("firstName")
                        //val middleName = jsonObject.getString("middleInitial")
                        //val lastName = jsonObject.getString("lastName")
                        //val mail = jsonObject.getString("mailAddress")
                        //val workNum = jsonObject.getString("workPhone")
                        val cellNum = jsonObject.getString("cellPhone")
                        //val jobName = jsonObject.getString("jobName")
                        //val departmentId = jsonObject.getInt("departmentId")
                        //val departmentName = jsonObject.getString("departmentName")
                        val email = jsonObject.getString("email")
                        //val province = jsonObject.getString("province")
                       // val country = jsonObject.getString("country")


             actionBar.subtitle = firstName
            val emailBtn: Button = findViewById(R.id.emailBtn)

            emailBtn.setOnClickListener{
                val intent = Intent(Intent.ACTION_SENDTO)
                intent.data = Uri.parse("mailto:$email")
                try{
                    startActivity(intent)
                } catch (e:Exception){
                    //error
                }
            }

            val callBtn: Button = findViewById(R.id.callBtn)

            callBtn.setOnClickListener{
                val intent = Intent(Intent.ACTION_DIAL)
                intent.data = Uri.parse("tel:$cellNum")
                try{
                    startActivity(intent)
                } catch (e:Exception){
                    //error
                }
            }
        }, {error ->
            Toast.makeText(this, "${error.message}", Toast.LENGTH_SHORT).show()
            Log.d(TAG, "API ERROR ${error.message}")
        })

        //add request in queue
        val requestQueue = Volley.newRequestQueue(this)
        requestQueue.add(stringRequest)
    }

    override fun onSupportNavigateUp(): Boolean {
        onBackPressed()
        return super.onSupportNavigateUp()
    }
}